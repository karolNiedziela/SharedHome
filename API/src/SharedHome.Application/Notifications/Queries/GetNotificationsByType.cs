using SharedHome.Notifications.DTO;
using SharedHome.Application.Common.Queries;

namespace SharedHome.Application.Notifications.Queries
{
    public class GetNotificationsByType : AuthorizedPagedQuery<AppNotificationDto>
    {
        public int? TargetType { get; set; }

        public int? NotificationType { get; set; }
    }
}
