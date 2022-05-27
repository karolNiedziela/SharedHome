using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.ShoppingLists.Exceptions
{
    public class ShoppingListNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListNotFound";

        public int Id { get; }

        public ShoppingListNotFoundException(int id) : base($"Shopping list with id '{id}' was not found.")
        {
            Id = id;
        }

    }
}
