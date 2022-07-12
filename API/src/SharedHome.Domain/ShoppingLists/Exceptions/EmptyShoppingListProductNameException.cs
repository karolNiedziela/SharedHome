using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class EmptyShoppingListProductNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyShoppingListProductName";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyShoppingListProductNameException() : base("Shopping list product cannot be empty.")
        {
        }
    }
}
