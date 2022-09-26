using SharedHome.Application.Common.Events;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Notifications.Services
{
    public class AppNotificationService : IAppNotificationService
    {
        private readonly IAppNotificationInformationResolver _notificationInformationResolver;
        private readonly INotificationRepository _notificationRepository;

        public AppNotificationService(IAppNotificationInformationResolver notificationInformationResolver, INotificationRepository notificationRepository)
        {
            _notificationInformationResolver = notificationInformationResolver;
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<AppNotification>> GetAll(string personId)
        {
            var notifications = await _notificationRepository.GetAll(personId);

            foreach (var notification in notifications)
            {
                notification.Title = _notificationInformationResolver.GetTitle(notification);
            }

            return notifications;
        }

        public Task AddAsync(AppNotification notification)
        {
            return Task.CompletedTask;
        }
    }
}
