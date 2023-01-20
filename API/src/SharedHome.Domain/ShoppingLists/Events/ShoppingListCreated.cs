using SharedHome.Domain.Primivites;

namespace SharedHome.Domain.ShoppingLists.Events
{
    public record ShoppingListCreated(Guid ShoppingListId, string ShoppingListName, Guid PersonId) : IDomainEvent;
}
