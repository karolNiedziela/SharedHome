using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteShoppingList
{
    public class DeleteShoppingListCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }
    }
}
