using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductAlreadyExistsException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductAlreadyExists";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        [Order]
        public string ProductName { get; }

        public ShoppingListProductAlreadyExistsException(string productName) 
            : base($"Shopping list product with name '{productName}' already added to list.")
        {
            ProductName = productName;
        }
    }
}
