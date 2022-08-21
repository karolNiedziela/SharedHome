using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListProductPriceChanged(int Id, string ProductName, Money Price) : IDomainEvent;
}
