using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Entities
{
    public class AppNotificationField
    {
        public int Id { get; set; }

        public AppNotificationFieldType Type { get; set; }

        public string Value { get; set; } = default!;

        public AppNotification AppNotification { get; set; } = default!;

        public int AppNotificationId { get; set; } = default!;

        private AppNotificationField()
        {

        }

        public AppNotificationField(AppNotificationFieldType type, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Field value cannot be empty.");
            }

            Type = type;
            Value = value;
        }
    }
}
