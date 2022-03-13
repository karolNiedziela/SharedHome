using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record DeleteShoppingList(int Id) : ICommand<Unit>;
}
