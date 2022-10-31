using SharedHome.Notifications.DTO;

namespace SharedHome.Application.Notifications.Hubs
{
    public interface IHouseGroupNotificationHubClient
    {
        Task BroadcastNotification(AppNotificationDto notifications);
    }
}
