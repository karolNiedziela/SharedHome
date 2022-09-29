using SharedHome.Notifications.Constants;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Services
{
    public interface IAppNotificationService
    {
        Task<IEnumerable<AppNotificationDto>> GetAll(string personId);

        Task AddAsync(AppNotification notification);

        Task BroadcastNotificationAsync(AppNotification notification, string personId, string personIdToExclude);
    }
}
