using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillNotPaidException : SharedHomeException
    {
        public override string ErrorCode => "BillNotPaid";

        public BillNotPaidException() : base($"Bill is not paid.")
        {
        }
    }
}
