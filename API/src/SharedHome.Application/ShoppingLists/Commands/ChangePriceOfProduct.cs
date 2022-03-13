using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record ChangePriceOfProduct(int ShoppingListId, string ProductName, decimal Price) : ICommand<Unit>;
}
