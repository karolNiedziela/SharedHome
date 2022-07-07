using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Tests.Shared.Providers
{
    public static class ShoppingListProvider
    {
        public const string PersonId = "c2506a12-41d4-4205-aafa-b835ae4bc057";
        public const string ShoppingListName = "ShoppingList";
        public const string ProductName = "Product";

        public static ShoppingList GetEmpty(bool isDone = false)        
            => ShoppingList.Create(ShoppingListName, PersonId, isDone);


        public static ShoppingList GetWithProduct(int quantity = 1, Money? price = null, NetContent? netContent = null, bool isBought = false)
        {
            var shoppingList = GetEmpty();

            shoppingList.AddProduct(GetProduct(quantity, price, netContent, isBought));

            return shoppingList;
        }
        public static ShoppingListProduct GetProduct(int quantity = 1, Money? price = null, NetContent? netContent = null, bool isBought = false)
            => new(ProductName, quantity, price, netContent, isBought);
    }
}
