using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record PurchaseProduct(int ShoppingListId, string ProductName, decimal Price) : ICommand;
}
