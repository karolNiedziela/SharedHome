using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public class ShoppingListProduct : ValueObject
    {
        public ShoppingListProductName Name { get; private set; } = default!;

        public Quantity Quantity { get; private set; } = default!;

        public ProductPrice? Price { get; private set; } = default!;

        public bool IsBought { get; private set; } = default!;

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

        public void Update(ShoppingListProduct shoppingListProduct)
        {
            Name = shoppingListProduct.Name;
            Quantity = shoppingListProduct.Quantity;
            Price = shoppingListProduct.Price;
            IsBought = shoppingListProduct.IsBought;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Quantity;
            if (Price is not null)
            {
                yield return Price;
            }
            yield return IsBought;
        }
    }
}
