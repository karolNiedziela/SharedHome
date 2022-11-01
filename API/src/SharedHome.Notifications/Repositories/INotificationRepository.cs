using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<AppNotification>> GetAllAsync(Guid personId, NotificationType? notificationType = null, TargetType? targetType = null);

        Task<IEnumerable<AppNotification>> GetAllAsync(Guid personId, IEnumerable<int> notificationIds);

        Task AddAsync(AppNotification notification);

        Task UpdateAsync(IEnumerable<AppNotification> notification);
    }
}
