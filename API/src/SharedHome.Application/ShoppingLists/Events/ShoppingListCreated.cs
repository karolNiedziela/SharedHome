using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListCreated(string ShoppingListName) : IDomainEvent;    
}
