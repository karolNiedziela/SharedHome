using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductWasNotBoughtException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductWasNotBought";

        public string ProductName { get; }

        public ShoppingListProductWasNotBoughtException(string productName) 
            : base($"Shopping list product '{productName}' was not bought.")
        {
            ProductName = productName;
        }

    }
}
