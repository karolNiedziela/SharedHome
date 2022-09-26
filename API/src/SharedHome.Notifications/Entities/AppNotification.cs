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

        public OperationType Operation { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = default!;

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; } = default!;

        public AppNotification()
        {

        }

        public AppNotification(string personId, string title, TargetType targetType, OperationType operationType, NotificationType? notificationType = null)
        {
            PersonId = personId;
            Title = title;
            Type = notificationType;
            Operation = operationType;
            Target = targetType;
            IsRead = false;
        }

        public AppNotification(string personId, string title, string message, TargetType targetType, OperationType operationType, NotificationType? notificationType = null)
        {
            PersonId = personId;
            Title = title;
            Message = message;
            Type = notificationType;
            Operation = operationType;
            Target = targetType;
            IsRead = false;
        }
    }
}
