using SharedHome.Notifications.Constants;

namespace SharedHome.Notifications.Validators
{
    public class DateOfPaymentFieldValidator : IAppNotificationFieldValidator
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
