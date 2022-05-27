using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class QuantityBelowZeroException : SharedHomeException
    {
        public override string ErrorCode => "QuantityBelowZero";

        public QuantityBelowZeroException() : base("Quantity cannot be lower than zero.")
        {
        }
    }
}
