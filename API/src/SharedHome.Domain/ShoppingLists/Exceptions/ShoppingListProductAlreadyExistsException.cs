using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductAlreadyExistsException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductAlreadyExists";

        [Order]
        public string ProductName { get; }

        public ShoppingListProductAlreadyExistsException(string productName) 
            : base($"Shopping list product with name '{productName}' already added to list.")
        {
            ProductName = productName;
        }
    }
}
