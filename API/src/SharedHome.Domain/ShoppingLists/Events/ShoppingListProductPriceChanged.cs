using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Events
{
    public record ShoppingListProductPriceChanged(int Id, string ProductName, ProductPrice Price) : IEvent;
}
