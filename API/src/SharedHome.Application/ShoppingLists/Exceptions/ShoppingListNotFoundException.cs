using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SharedHome.Application.ShoppingLists.Exceptions
{
    public class ShoppingListNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListNotFound";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        [Order]
        public int Id { get; }

        public ShoppingListNotFoundException(int id) : base($"Shopping list with id '{id}' was not found.")
        {
            Id = id;
        }
    }
}
