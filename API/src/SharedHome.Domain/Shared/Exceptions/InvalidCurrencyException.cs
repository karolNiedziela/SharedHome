using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.Shared.Exceptions
{
    public class InvalidCurrencyException : SharedHomeException
    {
        public override string ErrorCode => "InvalidCurrency";

        public InvalidCurrencyException() : base("Invalid currency.")
        {

        }
    }
}
