using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Shared.Exceptions
{
    public class MoneyAmountBelowZeroException : SharedHomeException
    {
        public override string ErrorCode => "MoneyAmountBelowZero";

        public MoneyAmountBelowZeroException() : base("Price cannot be lower than zero.")
        {
        }
    }
}
