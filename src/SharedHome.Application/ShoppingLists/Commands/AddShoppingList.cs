using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record AddShoppingList(Guid PersonId, string Name) : ICommand;
}
