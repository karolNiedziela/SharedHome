using SharedHome.Domain.Primivites;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Domain.ShoppingLists.Entities
{
    public sealed class ShoppingListProduct : Entity
    {
        public ShoppingListProductId Id { get; private set; } = default!;

        public ShoppingListProductName Name { get; private set; } = default!;

        public Quantity Quantity { get; private set; } = default!;

        public Money? Price { get; private set; } = default!;

        public NetContent? NetContent { get; private set; } = default!;

        public bool IsBought { get; private set; } = default!;

        private ShoppingListProduct()
        {

        }

        private ShoppingListProduct(
            ShoppingListProductName name,
            Quantity quantity,
            Money? price = null,
            NetContent? netContent = null,
            bool isBought = false,
            ShoppingListProductId? id = null)
        {
            Id = id ?? new ShoppingListProductId();
            Name = name;
            Quantity = quantity;
            Price = price;
            NetContent = netContent;
            IsBought = isBought;
        }

        public static ShoppingListProduct Create(
            ShoppingListProductName name,
            Quantity quantity,
            Money? price = null,
            NetContent? netContent = null,
            bool isBought = false,
            ShoppingListProductId? id = null)
        {
            return  new ShoppingListProduct(name, quantity, price, netContent, isBought, id);
        }

        public void PurchaseProduct(Money price)
        {
            Price = price;
            IsBought = true;
        }

        public void ChangePriceOfProduct(Money? price)
        {
            Price = price;
        }

        public void CancelPurchaseOfProduct()
        {
            Price = null;
            IsBought = false;
        }
        public void Update(ShoppingListProduct shoppingListProduct)
        {
            Name = shoppingListProduct.Name;
            Quantity = shoppingListProduct.Quantity;
            Price = shoppingListProduct.Price;
            NetContent = shoppingListProduct.NetContent;
            IsBought = shoppingListProduct.IsBought;
        }
    }
}
