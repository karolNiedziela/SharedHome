using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingList
{
    public class UpdateShoppingListCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public string Name { get; set; } = default!;
    }
}
