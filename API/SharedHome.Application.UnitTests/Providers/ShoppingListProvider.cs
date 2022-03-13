using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using System;

namespace SharedHome.Application.UnitTests.Providers
{
    public static class ShoppingListProvider
    {
        public const string DefaultPersonId = "c2506a12-41d4-4205-aafa-b835ae4bc057";
        public const string DefaultShoppingListName = "ShoppingList";
        public const string DefaultProductName = "Product";

        public static ShoppingList GetEmpty()        
            => ShoppingList.Create(DefaultShoppingListName, Guid.Parse(DefaultPersonId));


        public static ShoppingList GetWithProduct(int quantity = 1, ProductPrice? productPrice = null, bool isBought = false)
        {
            var shoppingList = GetEmpty();

            shoppingList.AddProduct(GetProduct(quantity, productPrice, isBought));

            return shoppingList;
        }
        public static ShoppingListProduct GetProduct(int quantity = 1, ProductPrice? productPrice = null, bool isBought = false)
            => new(DefaultProductName, quantity, productPrice, isBought);
    }
}
