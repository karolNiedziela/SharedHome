using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillCostBelowZeroException : SharedHomeException
    {
        public override string ErrorCode => "BillCostBelowZero";

        public BillCostBelowZeroException() : base("Bill cost cannot be lower than zero.")
        {
        }
    }
}
