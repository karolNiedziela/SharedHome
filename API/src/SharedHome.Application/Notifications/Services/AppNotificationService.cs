using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using SharedHome.Application.Notifications.Hubs;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;
using SharedHome.Notifications.Services;
using SharedHome.Notifications.Validators;

namespace SharedHome.Application.Notifications.Services
{
    internal class AppNotificationService : IAppNotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IAppNotificationFieldValidator> _validators;
        private readonly IHubContext<HouseGroupNotificationHub, IHouseGroupNotificationHubClient> _hubContext;
        private readonly IPersonRepository _personRepository;

        public AppNotificationService(
            INotificationRepository notificationRepository,
            IMapper mapper,
            IHubContext<HouseGroupNotificationHub, IHouseGroupNotificationHubClient> hubContext,
            IEnumerable<IAppNotificationFieldValidator> validators,
            IPersonRepository personRepository)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _validators = validators;
            _personRepository = personRepository;
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

            notification.CreatedByFullName = await GetPersonFullName(notification.CreatedBy);

            await _notificationRepository.AddAsync(notification);
        }

        public async Task BroadcastNotificationAsync(AppNotification notification, Guid personId)
        {
            var notificationDto = _mapper.Map<AppNotificationDto>(notification);

            await _hubContext.Clients.User(personId.ToString()).BroadcastNotificationAsync(notificationDto);
        }

        public async Task BroadcastNotificationAsync(AppNotification notification, Guid personId, Guid personIdToExclude)
        {
            var notificationDto = _mapper.Map<AppNotificationDto>(notification);

            if (!HouseGroupNotificationHub.GroupNames.TryGetValue(personId, out var groupName))
            {
                return;
            }

            await _hubContext.Clients.GroupExcept(
                groupName!, 
                HouseGroupNotificationHub.GetConnectionId(personIdToExclude))
                .BroadcastNotificationAsync(notificationDto);
        }

        private async Task<string> GetPersonFullName(Guid personId)
        {
            var person = await _personRepository.GetAsync(personId);
            if (person is null)
            {
                return string.Empty;
            }

            return $"{person.FirstName.Value} {person.LastName.Value}";
        }
    }
}
