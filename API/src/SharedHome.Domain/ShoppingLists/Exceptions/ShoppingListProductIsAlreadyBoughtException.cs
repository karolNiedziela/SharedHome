using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductIsAlreadyBoughtException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductIsAlreadyBought";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ProductName { get; }

        public ShoppingListProductIsAlreadyBoughtException(string productName)
            : base($"Shopping list product '{productName}' is already bought.")
        {
            ProductName = productName;
        }

    }
}
