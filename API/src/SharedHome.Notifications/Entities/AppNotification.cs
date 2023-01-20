using SharedHome.Domain.Primivites;
using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Entities
{
    public class AppNotification : Entity
    {
        public int Id { get; set; }

        public Guid PersonId { get; set; } = default!;

        public string Title { get; set; } = default!;

        public NotificationType? Type { get; set; }

        public bool IsRead { get; set; } = false;

        public IEnumerable<AppNotificationField> Fields { get; set; } = new List<AppNotificationField>();

        public AppNotification()
        {

        }

        public AppNotification(Guid personId, string title, IEnumerable<AppNotificationField> fields, NotificationType? type = null)
        {
            PersonId = personId;
            Title = title;
            Fields = fields;
            Type = type is null ? NotificationType.Other : type;
            IsRead = false;
        }
    }
}
