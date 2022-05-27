using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ProductPriceBelowZeroException : SharedHomeException
    {
        public override string ErrorCode => "ProductPriceBelowZero";

        public ProductPriceBelowZeroException() : base("Product price cannot be lower than zero.")
        {
        }
    }
}
