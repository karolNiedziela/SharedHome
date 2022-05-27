using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductAlreadyExistsException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductAlreadyExists";

        public string ShoppingListName { get; }

        public string ProductName { get; }

        public ShoppingListProductAlreadyExistsException(string shoppingListName, string productName) 
            : base($"Shopping list '{shoppingListName} already has product '{productName}'.")
        {
            ShoppingListName = shoppingListName;
            ProductName = productName;
        }
    }
}
