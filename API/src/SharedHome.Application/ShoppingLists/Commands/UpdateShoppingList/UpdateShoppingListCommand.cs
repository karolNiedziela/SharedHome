using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingList
{
    public class UpdateShoppingListCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public string Name { get; set; } = default!;
    }
}
