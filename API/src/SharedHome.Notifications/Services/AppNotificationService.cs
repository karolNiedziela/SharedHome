using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Hubs;
using SharedHome.Notifications.Repositories;

namespace SharedHome.Notifications.Services
{
    public class AppNotificationService : IAppNotificationService
    {
        private readonly IAppNotificationInformationResolver _notificationInformationResolver;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<HouseGroupNotificationHub, IHouseGroupNotificationHubClient> _hubContext;

        public AppNotificationService(IAppNotificationInformationResolver notificationInformationResolver, INotificationRepository notificationRepository, IMapper mapper, IHubContext<HouseGroupNotificationHub, IHouseGroupNotificationHubClient> hubContext)
        {
            _notificationInformationResolver = notificationInformationResolver;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<AppNotificationDto>> GetAllAsync(string personId)
        {
            var notifications = await _notificationRepository.GetAllAsync(personId);

            foreach (var notification in notifications)
            {
                notification.Title = _notificationInformationResolver.GetTitle(notification);
            }

            return _mapper.Map<IEnumerable<AppNotificationDto>>(notifications);
        }
        public Task AddAsync(AppNotification notification)
        {
            return Task.CompletedTask;
        }

        public async Task BroadcastNotificationAsync(AppNotification notification, string personId, string personIdToExclude, string? name = null)
        {
            var notificationDto = _mapper.Map<AppNotificationDto>(notification);

            if (!HouseGroupNotificationHub.GroupNames.TryGetValue(personId, out var groupName))
            {
                return;
            }

            notificationDto.Title = _notificationInformationResolver.GetTitle(notification, name);

            await _hubContext.Clients.GroupExcept(groupName!, HouseGroupNotificationHub.GetConnectionId(personIdToExclude)).BroadcastNotification(notificationDto);
        }
    }
}
