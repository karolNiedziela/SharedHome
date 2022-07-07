using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.ShoppingLists.Events
{
    public record ShoppingListProductPurchased(int ShoppingListId, string ProductName, Money Price) : IEvent;
}
