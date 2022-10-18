using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<AppNotification>> GetAllAsync(string personId, NotificationType? notificationType = null, TargetType? targetType = null);

        Task AddAsync(AppNotification notification);
    }
}
