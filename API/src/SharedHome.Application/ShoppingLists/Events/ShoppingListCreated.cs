using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Common.Events;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListCreated(Guid ShoppingListId, string ShoppingListName, CreatorDto Creator) : IDomainEvent;    
}
