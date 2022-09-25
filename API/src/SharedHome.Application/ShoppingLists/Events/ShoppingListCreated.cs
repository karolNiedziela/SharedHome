using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListCreated(int ShoppingListId, string ShoppingListName, CreatorDto Creator) : IDomainEvent;    
}
