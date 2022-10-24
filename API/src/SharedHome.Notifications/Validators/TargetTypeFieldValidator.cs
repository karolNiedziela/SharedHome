using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Validators
{
    internal class TargetTypeFieldValidator : IAppNotificationFieldValidator
    {
        public AppNotificationFieldType FieldType { get; }

        public TargetTypeFieldValidator()
        {
            FieldType = AppNotificationFieldType.Target;
        }

        public bool IsValid(string fieldValue)
            => Enum.IsDefined(typeof(TargetType), fieldValue);
    }
}
