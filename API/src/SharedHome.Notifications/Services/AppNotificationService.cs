using MapsterMapper;
using SharedHome.Application.Common.Events;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Notifications.Services
{
    public class AppNotificationService : IAppNotificationService
    {
        private readonly IAppNotificationInformationResolver _notificationInformationResolver;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public AppNotificationService(IAppNotificationInformationResolver notificationInformationResolver, INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationInformationResolver = notificationInformationResolver;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppNotificationDto>> GetAll(string personId)
        {
            var notifications = await _notificationRepository.GetAll(personId);

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
    }
}
