using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Services
{
    public interface IAppNotificationService
    {
        Task AddAsync(AppNotification notification);

        Task BroadcastNotificationAsync(AppNotification notification, Guid personId);

        Task BroadcastNotificationAsync(AppNotification notification, Guid personId, Guid personIdToExclude);
    }
}
