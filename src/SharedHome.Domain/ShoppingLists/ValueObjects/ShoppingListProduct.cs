using SharedHome.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record ShoppingListProduct
    {
        public ShoppingListProductName Name { get; }

        public Quantity Quantity { get; }

        public Money? Price { get; init; }

        public bool IsBought { get; init; }

        public ShoppingListProduct(ShoppingListProductName name, Quantity quantity, Money? price = null, bool isBought = false)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            IsBought = isBought;
        }
    }
}
