using SharedHome.Notifications.DTO;

namespace SharedHome.Notifications.Services
{
    public interface IHouseGroupNotificationHubClient
    {
        Task BroadcastNotification(AppNotificationDto notifications);
    }
}
