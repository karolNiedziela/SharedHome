using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone
{
    public class SetIsShoppingListDoneCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public bool IsDone { get; set; }
    }
}
