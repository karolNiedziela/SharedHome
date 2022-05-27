using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillPaidException : SharedHomeException
    {
        public override string ErrorCode => "BillPaid";

        public BillPaidException() : base($"Bill is already paid.")
        {
        }
    }
}
