using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductIsAlreadyBoughtException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductIsAlreadyBought";

        public string ProductName { get; }

        public ShoppingListProductIsAlreadyBoughtException(string productName)
            : base($"Shopping list product '{productName}' is already bought.")
        {
            ProductName = productName;
        }

    }
}
