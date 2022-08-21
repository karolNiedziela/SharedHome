using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.ShoppingLists.Events
{
    public record ShoppingListProductPurchaseCanceled(int ShoppingListId, string ProductName) : IDomainEvent;
}
