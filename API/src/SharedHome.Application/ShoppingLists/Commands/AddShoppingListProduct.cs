using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record AddShoppingListProduct(int ShoppingListId, string Name, int Quantity) : ICommand<Unit>;
}
