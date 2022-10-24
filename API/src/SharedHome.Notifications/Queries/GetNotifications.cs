using SharedHome.Notifications.DTO;
using SharedHome.Shared.Abstractions.Queries;

namespace SharedHome.Notifications.Queries
{
    public class GetNotifications : AuthorizedPagedQuery<AppNotificationDto>
    {
    }
}
