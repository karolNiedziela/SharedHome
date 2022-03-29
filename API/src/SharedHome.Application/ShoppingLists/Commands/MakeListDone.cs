using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public class MakeListDone : AuthorizeCommand, ICommand<Unit>
    {
        public int ShoppingListId { get; set; }
    }
}
