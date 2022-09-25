using SharedHome.Notifications.Constants;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Notifications.Entities
{
    public class AppNotification : IAuditable
    {
        public int Id { get; private set; }

        public string PersonId { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? Message { get; set; }

        public NotificationType? Type { get; set; }

        public TargetType Target { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public AppNotification()
        {

        }

        public AppNotification(string personId, string title, TargetType targetType, NotificationType? notificationType = null)
        {
            PersonId = personId;
            Title = title;
            Type = notificationType;
            Target = targetType;
            IsRead = false;
        }

        public AppNotification(string personId, string title, string message, TargetType targetType, NotificationType? notificationType = null)
        {
            PersonId = personId;
            Title = title;
            Message = message;
            Type = notificationType;
            Target = targetType;
            IsRead = false;
        }
    }
}
