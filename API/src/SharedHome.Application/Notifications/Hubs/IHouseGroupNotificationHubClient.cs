using SharedHome.Notifications.DTO;

namespace SharedHome.Application.Notifications.Hubs
{
    public interface IHouseGroupNotificationHubClient
    {
        Task BroadcastNotificationAsync(AppNotificationDto notifications);
    }
}
