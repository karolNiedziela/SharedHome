using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Hubs;
using SharedHome.Notifications.Repositories;
using SharedHome.Notifications.Validators;

namespace SharedHome.Notifications.Services
{
    internal class AppNotificationService : IAppNotificationService
    {
        private readonly IAppNotificationInformationResolver _notificationInformationResolver;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IAppNotificationFieldValidator> _validators;
        private readonly IHubContext<HouseGroupNotificationHub, IHouseGroupNotificationHubClient> _hubContext;

        public AppNotificationService(
            IAppNotificationInformationResolver notificationInformationResolver,
            INotificationRepository notificationRepository,
            IMapper mapper,
            IHubContext<HouseGroupNotificationHub, IHouseGroupNotificationHubClient> hubContext,
            IEnumerable<IAppNotificationFieldValidator> validators)
        {
            _notificationInformationResolver = notificationInformationResolver;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _validators = validators;
        }
       
        public async Task AddAsync(AppNotification notification)
        {
            var isValid = true;
            foreach (var validator in _validators)
            {
                foreach (var field in notification.Fields)
                {
                    if (field.Type == validator.FieldType)
                    {                        
                        isValid = validator.IsValid(field.Value);
                    }

                    if (!isValid)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (!isValid)
            {
                throw new Exception("Invalid field");
            }

            await _notificationRepository.AddAsync(notification);
        }

        public async Task BroadcastNotificationAsync(AppNotification notification, Guid personId, Guid personIdToExclude)
        {
            var notificationDto = _mapper.Map<AppNotificationDto>(notification);

            if (!HouseGroupNotificationHub.GroupNames.TryGetValue(personId, out var groupName))
            {
                return;
            }

            await _hubContext.Clients.GroupExcept(groupName!, HouseGroupNotificationHub.GetConnectionId(personIdToExclude)).BroadcastNotification(notificationDto);
        }
    }
}
