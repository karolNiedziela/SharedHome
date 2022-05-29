using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SharedHome.Application.ShoppingLists.Exceptions
{
    public class ShoppingListNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListNotFound";

        [Order]
        public int Id { get; }

        public ShoppingListNotFoundException(int id) : base($"Shopping list with id '{id}' was not found.")
        {
            Id = id;
        }
    }
}
