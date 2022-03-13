using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record UndoListDone(int ShoppingListId) : ICommand<Unit>;

}
