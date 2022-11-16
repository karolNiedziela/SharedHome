using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class EmptyShoppingListNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyShoppingListName";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EmptyShoppingListNameException() : base("Shopping list name cannot be empty.")
        {
        }
    }
}
