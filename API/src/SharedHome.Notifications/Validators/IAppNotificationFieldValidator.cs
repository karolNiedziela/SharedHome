using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Validators
{
    public interface IAppNotificationFieldValidator
    {
        AppNotificationFieldType FieldType { get; }

        bool IsValid(string fieldValue);
    }
}
