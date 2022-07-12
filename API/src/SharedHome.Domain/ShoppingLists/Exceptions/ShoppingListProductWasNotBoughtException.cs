using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductWasNotBoughtException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductWasNotBought";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ProductName { get; }

        public ShoppingListProductWasNotBoughtException(string productName) 
            : base($"Shopping list product '{productName}' was not bought.")
        {
            ProductName = productName;
        }

    }
}
