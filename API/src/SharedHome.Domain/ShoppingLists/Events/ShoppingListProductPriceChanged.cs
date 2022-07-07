using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.ShoppingLists.Events
{
    public record ShoppingListProductPriceChanged(int Id, string ProductName, Money Price) : IEvent;
}
