using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Services
{
    public interface IAppNotificationService
    {
        Task<IEnumerable<AppNotificationDto>> GetAllAsync(Guid personId);

        Task AddAsync(AppNotification notification);

        Task BroadcastNotificationAsync(AppNotification notification, Guid personId, Guid personIdToExclude, string? name = null);
    }
}
