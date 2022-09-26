using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Services
{
    public interface IAppNotificationService
    {
        Task<IEnumerable<AppNotification>> GetAll(string personId);

        Task AddAsync(AppNotification notification);
    }
}
