using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListProductAdded(int ShoppingListId, string ProductName) : IDomainEvent;
}
