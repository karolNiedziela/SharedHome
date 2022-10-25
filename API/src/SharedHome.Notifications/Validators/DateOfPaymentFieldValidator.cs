using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Validators
{
    internal class DateOfPaymentFieldValidator : IAppNotificationFieldValidator
    {
        public AppNotificationFieldType FieldType { get; }

        public DateOfPaymentFieldValidator()
        {
            FieldType = AppNotificationFieldType.DateOfPayment;
        }

        public bool IsValid(string fieldValue)
            => !string.IsNullOrEmpty(fieldValue);
    }
}
