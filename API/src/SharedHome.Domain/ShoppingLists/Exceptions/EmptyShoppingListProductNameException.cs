using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class EmptyShoppingListProductNameException : SharedHomeException
    {
        public override string ErrorCode => "EmptyShoppingListProductName";

        public EmptyShoppingListProductNameException() : base("Shopping list product cannot be empty.")
        {
        }
    }
}
