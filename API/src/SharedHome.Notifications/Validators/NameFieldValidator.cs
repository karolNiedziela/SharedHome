using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;

namespace SharedHome.Notifications.Validators
{
    public class NameFieldValidator : IAppNotificationFieldValidator
    {
        public AppNotificationFieldType FieldType { get; }

        public NameFieldValidator()
        {
            FieldType = AppNotificationFieldType.Name;
        }

        public bool IsValid(string fieldValue)
            => !string.IsNullOrEmpty(fieldValue);
    }
}
