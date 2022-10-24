using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Validators
{
    internal class OperationTypeFieldValidator : IAppNotificationFieldValidator
    {
        public AppNotificationFieldType FieldType { get; }

        public OperationTypeFieldValidator()
        {
            FieldType = AppNotificationFieldType.Operation;
        }

        public bool IsValid(string fieldValue)
               => Enum.IsDefined(typeof(OperationType), fieldValue);
    }
}
