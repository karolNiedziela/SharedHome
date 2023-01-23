using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Tests.Shared.Providers
{
    public static class ShoppingListFakeProvider
    {
        public static readonly Guid ShoppingListId = new("4bb7ae03-1cf9-4355-ab11-d2d13718e791");
        public static readonly Guid PersonId = new("c2506a12-41d4-4205-aafa-b835ae4bc057");
        public static readonly DateTime CreationDate = new(2022, 10, 10);
        public const string ShoppingListName = "ShoppingList";
        public const string ProductName = "Product";

        public static ShoppingList GetEmpty(DateTime? creationDate = null, Guid? personId = null, Guid? shoppingListId = null)        
            => ShoppingList.Create(shoppingListId ?? ShoppingListId, ShoppingListName, personId ?? PersonId, creationDate ?? CreationDate);


        public static ShoppingList GetWithProduct(
            int quantity = 1,
            Money? price = null,
            NetContent? netContent = null,
            bool isBought = false,
            Guid? personId = null,
            Guid? shoppingListId = null)
        {
            var shoppingList = GetEmpty(personId: personId, shoppingListId: shoppingListId);

            shoppingList.AddProduct(GetProduct(quantity, price, netContent, isBought));

            return shoppingList;
        }
        public static ShoppingListProduct GetProduct(
            int quantity = 1,
            Money? price = null,
            NetContent? netContent = null,
            bool isBought = false,
            Guid? personId = null)
            => ShoppingListProduct.Create(ProductName, quantity, price, netContent, isBought, personId);
    }
}
