using SharedHome.Notifications.DTO;

namespace SharedHome.Notifications.Services
{
    public interface IHubClient
    {
        Task BroadcastNotification(AppNotificationDto notifications);
    }
}
