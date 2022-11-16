using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteShoppingList
{
    public class DeleteShoppingListCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid ShoppingListId { get; set; }
    }
}
