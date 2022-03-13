using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record MakeListDone(int ShoppingListId) : ICommand<Unit>;
}
