using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListProductRemoved(int ShoppingListId, string ProductName) : IDomainEvent;
    
}
