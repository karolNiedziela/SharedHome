using SharedHome.Notifications.DTO;
using SharedHome.Application.Common.Queries;

namespace SharedHome.Application.Notifications.Queries
{
    public class GetNotifications : AuthorizedPagedQuery<AppNotificationDto>
    {
    }
}
