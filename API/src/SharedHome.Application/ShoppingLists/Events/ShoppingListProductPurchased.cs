using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListProductPurchased(int ShoppingListId, string ProductName, Money Price) : IDomainEvent;
}
