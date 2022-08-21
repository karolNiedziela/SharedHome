using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListDone(int ShoppingListId) : IDomainEvent;
}
