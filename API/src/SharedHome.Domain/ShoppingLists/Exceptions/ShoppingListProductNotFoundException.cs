using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ProductName { get; }

        public ShoppingListProductNotFoundException(string productName) 
            : base($"Shopping list product '{productName}' was not found.")
        {
            ProductName = productName;
        }
    }
}
