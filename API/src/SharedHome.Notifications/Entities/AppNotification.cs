using SharedHome.Domain.Primivites;
using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Entities
{
    public class AppNotification
    {
        public int Id { get; set; }

        public Guid PersonId { get; set; } = default!;

        public string Title { get; set; } = default!;

        public NotificationType? Type { get; set; }

        public bool IsRead { get; set; } = false;

        public IEnumerable<AppNotificationField> Fields { get; set; } = new List<AppNotificationField>();

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByFullName { get; set; } = default!;

        public AppNotification()
        {

        }

        public AppNotification(Guid personId, string title, IEnumerable<AppNotificationField> fields, Guid createdBy, NotificationType? type = null)
        {
            PersonId = personId;
            Title = title;
            Fields = fields;
            Type = type is null ? NotificationType.Other : type;
            IsRead = false;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
