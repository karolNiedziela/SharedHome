using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class EmptyShoppingListNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyShoppingListName";

        public EmptyShoppingListNameException() : base("Shopping list name cannot be empty.")
        {
        }
    }
}
