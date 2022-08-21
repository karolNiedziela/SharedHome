using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListUndone(int ShoppingListId) : IDomainEvent;
}
