using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record ShoppingListProduct
    {
        public ShoppingListProductName Name { get; } = default!;

        public Quantity Quantity { get; } = default!;

        public ProductPrice? Price { get; init; } = default!;

        public bool IsBought { get; init; } = default!;

        private ShoppingListProduct()
        {

        }

        public ShoppingListProduct(ShoppingListProductName name, Quantity quantity, ProductPrice? price = null, bool isBought = false)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            IsBought = isBought;
        }
    }
}
