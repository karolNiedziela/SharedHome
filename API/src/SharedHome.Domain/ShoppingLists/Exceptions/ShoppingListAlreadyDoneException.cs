using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListAlreadyDoneException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListAlreadyDone";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public ShoppingListAlreadyDoneException() : base("Shopping list is already done.")
        {
        }
    }
}
