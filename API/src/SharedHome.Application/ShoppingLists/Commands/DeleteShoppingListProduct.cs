using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record DeleteShoppingListProduct(int ShoppingListId, string ProductName) : ICommand<Unit>;
}
