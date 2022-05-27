using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductNotFound";

        public string ProductName { get; }

        public ShoppingListProductNotFoundException(string productName) 
            : base($"Shopping list product '{productName}' was not found.")
        {
            ProductName = productName;
        }
    }
}
