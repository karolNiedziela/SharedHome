using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record ShoppingListProduct
    {
        public ShoppingListProductName Name { get; private set; } = default!;

        public Quantity Quantity { get; private set; } = default!;

        public Money? Price { get; private set; } = default!;

        public NetContent? NetContent { get; private set; } = default!;

        public bool IsBought { get; private set; } = default!;

        private ShoppingListProduct()
        {

        }

        public ShoppingListProduct(ShoppingListProductName name, Quantity quantity, Money? price = null, NetContent? netContent = null, bool isBought = false)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            NetContent = netContent;
            IsBought = isBought;
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
