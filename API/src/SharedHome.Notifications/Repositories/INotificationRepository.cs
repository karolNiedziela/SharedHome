using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<AppNotification>> GetAll(NotificationType? notificationType, TargetType? targetType);

        Task AddAsync(AppNotification notification);
    }
}
