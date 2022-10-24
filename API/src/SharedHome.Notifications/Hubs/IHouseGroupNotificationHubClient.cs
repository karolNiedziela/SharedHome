using SharedHome.Notifications.DTO;

namespace SharedHome.Notifications.Hubs
{
    public interface IHouseGroupNotificationHubClient
    {
        Task BroadcastNotification(AppNotificationDto notifications);
    }
}
