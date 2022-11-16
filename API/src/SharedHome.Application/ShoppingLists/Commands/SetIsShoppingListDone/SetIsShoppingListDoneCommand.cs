using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone
{
    public class SetIsShoppingListDoneCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public bool IsDone { get; set; }
    }
}
