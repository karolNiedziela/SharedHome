using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Validators
{
    internal interface IAppNotificationFieldValidator
    {
        AppNotificationFieldType FieldType { get; }

        bool IsValid(string fieldValue);
    }
}
