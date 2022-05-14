﻿using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Events;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Domain.UnitTests.ShoppingLists
{
    public class ShoppingListsTests
    {
        private string _personId = "46826ecb-c40d-441c-ad0d-f11e616e4948";

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_Throws_EmptyShoppingListNameException_When_ShoppingListName_Is_NullOrWhiteSpace(string shoppingListName)
        {
            var exception = Record.Exception(() => ShoppingList.Create(shoppingListName, _personId));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyShoppingListNameException>();
        }

        [Fact]
        public void Create_Throws_TooLongShoppingListNameException_When_ShoppingListName_Is_Longer_Than_20_Characters()
        {
            var exception = Record.Exception(() => ShoppingList.Create(new string('k', 25), _personId));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<TooLongShoppingListNameException>();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddProduct_Throws_EmptyShoppingListProductNameException_When_ShoppingListProductName_Is_NullOrWhiteSpace(string shoppingListProductName)
        {
            var shoppingList = GetShoppingList();

            var exception = Record.Exception(() => shoppingList.AddProduct(new ShoppingListProduct(shoppingListProductName, 1)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyShoppingListProductNameException>();
        }

        [Fact]
        public void AddProduct_Throws_QuantityBelowZeroException_When_Quantity_Is_Lower_Than_Zero()
        {
            var shoppingList = GetShoppingList();

            var exception = Record.Exception(() => shoppingList.AddProduct(new ShoppingListProduct("Product", -5)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<QuantityBelowZeroException>();
        }

        [Fact]
        public void AddProduct_Throws_ProductPriceBelowZeroException_When_ProductPrice_Is_Lower_Than_Zero()
        {
            var shoppingList = GetShoppingList();

            var exception = Record.Exception(() => shoppingList.AddProduct(new ShoppingListProduct("Product", 2, -10)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ProductPriceBelowZeroException>();
        }

        [Fact]
        public void AddProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = GetShoppingList(true);

            var exception = Record.Exception(() => shoppingList.AddProduct(new ShoppingListProduct("product", 1)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void AddProduct_Throws_ShoppingListProductAlreadyExistsException_When_Product_Already_Exists_In_Shopping_List()
        {
            var shoppingList = GetShoppingList();
            var product = GetProduct();
            shoppingList.AddProduct(product);

            var exception = Record.Exception(() => shoppingList.AddProduct(product));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductAlreadyExistsException>();
        }

        [Fact]
        public void AddProduct_Adds_ShoppingListProductAdded_On_Success()
        {
            var shoppingList = GetShoppingList();
            var product = GetProduct();

            shoppingList.AddProduct(product);

            shoppingList.Events.Count().ShouldBe(1);

            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductAdded;
            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe("product");
        }

        [Fact]
        public void AddProducts_Adds_ShoppingListProductAdded_On_Success()
        {
            var shoppingList = GetShoppingList();
            var products = new List<ShoppingListProduct>
            {
                new ShoppingListProduct("Product 1", 1),
                new ShoppingListProduct("Product 2", 2),
            };

            shoppingList.AddProducts(products);

            shoppingList.Products.Count().ShouldBe(2);
        }

        [Fact]
        public void RemoveProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = GetShoppingList(true);

            var exception = Record.Exception(() => shoppingList.RemoveProduct("product"));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void RemoveProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shoppingList = GetShoppingList();

            var exception = Record.Exception(() => shoppingList.RemoveProduct("product"));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void RemoveProduct_Adds_ShoppingListProductRemoved_On_Success()
        {
            var shoppingList = GetShoppingList();
            var product = GetProduct();
            shoppingList.AddProduct(product);
            shoppingList.ClearEvents();

            shoppingList.RemoveProduct("product");

            shoppingList.Events.Count().ShouldBe(1);

            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductRemoved;
            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe("product");
        }

        [Fact]
        public void PurchaseProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = GetShoppingList(true);

            var exception = Record.Exception(() => shoppingList.PurchaseProduct("product", 10));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void PurchaseProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shoppingList = GetShoppingList();

            var exception = Record.Exception(() => shoppingList.PurchaseProduct("product", 10));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void PurchaseProduct_Throws_ShoppingListProductIsAlreadyBoughtException_When_Product_Is_Already_Bought()
        {
            var shoppingList = GetShoppingList();

            var product = GetProduct(25, true);
            shoppingList.AddProduct(product);
            shoppingList.ClearEvents();

            var exception = Record.Exception(() => shoppingList.PurchaseProduct("product", 10m));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductIsAlreadyBoughtException>();
        }

        [Fact]
        public void PurchaseProduct_Adds_ShoppingListProductPurchased_On_Success()
        {
            var shoppingList = GetShoppingList();

            var product = GetProduct();
            shoppingList.AddProduct(product);
            shoppingList.ClearEvents();

            shoppingList.PurchaseProduct("product", 10m);

            shoppingList.Events.Count().ShouldBe(1);

            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductPurchased;
            @event.ShouldNotBeNull();

            @event.ProductName.ShouldBe("product");
            @event.Price.ShouldBe(10m);
        }

        [Fact]
        public void ChangePriceOfProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = GetShoppingList(true);

            var exception = Record.Exception(() => shoppingList.ChangePriceOfProduct("product", 10m));
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void ChangePriceOfProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shoppingList = GetShoppingList();

            var exception = Record.Exception(() => shoppingList.ChangePriceOfProduct("product", 10m));
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void ChangePriceOfProduct_Throws_ShoppingListProductWasNotBoughtException_When_Product_Is_Not_Bought()
        {
            var shoppingList = GetShoppingList();

            var product = GetProduct();
            shoppingList.AddProduct(product);
            var exception = Record.Exception(() => shoppingList.ChangePriceOfProduct("product", 10m));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductWasNotBoughtException>();
        }

        [Fact]
        public void ChangePriceOfProduct_Adds_ShoppingListProductPriceChanged_On_Success()
        {
            var shoppingList = GetShoppingList();
            var product = GetProduct();

            shoppingList.AddProduct(product);
            shoppingList.PurchaseProduct("product", 10m);
            shoppingList.ClearEvents();

            shoppingList.ChangePriceOfProduct("product", 25m);

            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductPriceChanged;
            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe("product");
            @event.Price.ShouldBe(25);
        }

        [Fact]
        public void CancelPurchaseOfProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = GetShoppingList(true);

            var exception = Record.Exception(() => shoppingList.CancelPurchaseOfProduct("product"));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void CancelPurchaseOfProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shopping = GetShoppingList();

            var exception = Record.Exception(() => shopping.CancelPurchaseOfProduct("product"));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void CancelPurchaseOfProduct_Throws_ShoppingListProductWasNotBoughtException_When_Product_Is_Not_Bought()
        {
            var shoppingList = GetShoppingList();
            var product = GetProduct(isBought: false);
            shoppingList.AddProduct(product);

            var exception = Record.Exception(() => shoppingList.CancelPurchaseOfProduct("product"));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductWasNotBoughtException>();
        }

        [Fact]
        public void CancelPurchaseOfProduct_Adds_ShoppingListProductPurchaseCanceled_On_Success()
        {
            var shoppingList = GetShoppingList();
            var product = GetProduct(isBought: false);
            shoppingList.AddProduct(product);

            shoppingList.PurchaseProduct("product", 10m);
            shoppingList.ClearEvents();

            shoppingList.CancelPurchaseOfProduct("product");

            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductPurchaseCanceled;

            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe("product");
        }

        [Fact]
        public void MakeListDone_Throws_ShoppingListAlreadyDoneException_When_Is_Already_Done()
        {
            var shoppingList = GetShoppingList(isDone: true);

            var exception = Record.Exception(() => shoppingList.MakeListDone());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void MakeListDone_Adds_ShoppingListDone_On_Success()
        {
            var shoppingList = GetShoppingList();

            shoppingList.MakeListDone();

            shoppingList.IsDone.ShouldBeTrue();
            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListDone;
            @event.ShouldNotBeNull();
        }

        [Fact]
        public void UndoListDone_Adds_ShoppingListUndone_On_Success()
        {
            var shoppingList = GetShoppingList(isDone: false);

            shoppingList.UndoListDone();
            shoppingList.IsDone.ShouldBeFalse();
            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListUndone;
            @event.ShouldNotBeNull();
        }

        private ShoppingList GetShoppingList(bool isDone = false)
        {
            var shoppingList = ShoppingList.Create("shopping list", _personId, isDone);
            shoppingList.ClearEvents();

            return shoppingList;
        }

        private static ShoppingListProduct GetProduct(ProductPrice? price = null, bool isBought = false)
        {
            var product = new ShoppingListProduct("product", 1, price: price, isBought: isBought);

            return product;
        }
    }
}
