using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListAlreadyDoneException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListAlreadyDone";

        public ShoppingListAlreadyDoneException() : base("Shopping list is already done.")
        {
        }
    }
}
