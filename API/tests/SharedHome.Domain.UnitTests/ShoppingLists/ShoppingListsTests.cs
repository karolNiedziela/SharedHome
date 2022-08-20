using SharedHome.Domain.Shared.Exceptions;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Events;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SharedHome.Domain.UnitTests.ShoppingLists
{
    public class ShoppingListsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_Throws_EmptyShoppingListNameException_When_ShoppingListName_Is_NullOrWhiteSpace(string shoppingListName)
        {
            var exception = Record.Exception(() 
                => ShoppingList.Create(shoppingListName, ShoppingListProvider.PersonId));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyShoppingListNameException>();
        }

        [Fact]
        public void Create_Throws_TooLongShoppingListNameException_When_ShoppingListName_Is_Longer_Than_20_Characters()
        {
            var exception = Record.Exception(() 
                => ShoppingList.Create(new string('k', 25), ShoppingListProvider.PersonId));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<TooLongShoppingListNameException>();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddProduct_Throws_EmptyShoppingListProductNameException_When_ShoppingListProductName_Is_NullOrWhiteSpace(string shoppingListProductName)
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(new ShoppingListProduct(shoppingListProductName, 1)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyShoppingListProductNameException>();
        }

        [Fact]
        public void AddProduct_Throws_QuantityBelowZeroException_When_Quantity_Is_Lower_Than_Zero()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(new ShoppingListProduct("Product", -5)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<QuantityBelowZeroException>();
        }

        [Fact]
        public void AddProduct_Throws_MoneyAmountBelowZeroException_When_ProductPrice_Is_Lower_Than_Zero()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(new ShoppingListProduct("Product", 2, new Money(-10m, "zł"))));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<MoneyAmountBelowZeroException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AddProdcut_Throws_InvalidCurrencyException_When_Currency_Is_NullOrEmpty(string currency)
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(()
                => shoppingList.AddProduct(new ShoppingListProduct("Product", 2, new Money(10m, currency))));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidCurrencyException>();
        }

        [Fact]
        public void Add_Product_Throws_UnsupportedCurrencyException_When_Currency_Is_Not_In_AllowedValues()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(()
                => shoppingList.AddProduct(new ShoppingListProduct("Product", 2, new Money(10m, "GBR"))));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<UnsupportedCurrencyException>();
        }     

        [Fact]
        public void AddProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(true);

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(new ShoppingListProduct(ShoppingListProvider.ProductName, 1)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void AddProduct_Throws_ShoppingListProductAlreadyExistsException_When_Product_Already_Exists_In_Shopping_List()
        {
            var shoppingList = ShoppingListProvider.GetWithProduct();
            var product = ShoppingListProvider.GetProduct();

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(product));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductAlreadyExistsException>();
        }

        [Fact]
        public void AddProduct_Adds_ShoppingListProductAdded_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct();

            shoppingList.AddProduct(product);

            shoppingList.Events.Count().ShouldBe(1);

            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductAdded;
            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe(ShoppingListProvider.ProductName);
        }

        [Fact]
        public void AddProducts_Adds_ShoppingListProductAdded_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
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
            var shoppingList = ShoppingListProvider.GetEmpty(true);

            var exception = Record.Exception(() 
                => shoppingList.RemoveProduct(ShoppingListProvider.ProductName));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void RemoveProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.RemoveProduct(ShoppingListProvider.ProductName));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void RemoveProduct_Adds_ShoppingListProductRemoved_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct();
            shoppingList.AddProduct(product);
            shoppingList.ClearEvents();

            shoppingList.RemoveProduct(ShoppingListProvider.ProductName);

            shoppingList.Events.Count().ShouldBe(1);

            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductRemoved;
            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe(ShoppingListProvider.ProductName);
        }

        [Fact]
        public void RemoveProducts_Remove_Products_With_Given_ProductNames()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            shoppingList.AddProduct(new ShoppingListProduct("Produkt1", 1));
            shoppingList.AddProduct(new ShoppingListProduct("Produkt2", 1));
            shoppingList.AddProduct(new ShoppingListProduct("Produkt3", 1));

            shoppingList.RemoveProducts(new[] { "Produkt1", "Produkt3" });

            shoppingList.Products.Count().ShouldBe(1);
            shoppingList.Products.First().Name.Value.ShouldBe("Produkt2");
        }

        [Fact]
        public void PurchaseProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(true);

            var exception = Record.Exception(() 
                => shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void PurchaseProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void PurchaseProduct_Throws_ShoppingListProductIsAlreadyBoughtException_When_Product_Is_Already_Bought()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var product = ShoppingListProvider.GetProduct(price: new Money(25m, "zł"), isBought: true);
            shoppingList.AddProduct(product);
            shoppingList.ClearEvents();

            var exception = Record.Exception(() 
                => shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductIsAlreadyBoughtException>();
        }

        [Fact]
        public void PurchaseProduct_Adds_ShoppingListProductPurchased_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var product = ShoppingListProvider.GetProduct();
            shoppingList.AddProduct(product);
            shoppingList.ClearEvents();

            shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł"));

            shoppingList.Events.Count().ShouldBe(1);

            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductPurchased;
            @event.ShouldNotBeNull();

            @event.ProductName.ShouldBe(ShoppingListProvider.ProductName);
            @event.Price.Amount.ShouldBe(10m);
            @event.Price.Currency.Value.ShouldBe("zł");
        }

        [Fact]
        public void PurchaseProducts_Set_Price_And_Is_Bought_To_True()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            shoppingList.AddProducts(new[]
            {
                new ShoppingListProduct("Product", 1),
                new ShoppingListProduct("Product2", 1),
            });

            shoppingList.PurchaseProducts(new Dictionary<string, Money>
            {
                { "Product", new Money(20, "zł") },
                { "Product2", new Money(30, "zł") },
            });

            shoppingList.Products.First().IsBought.ShouldBeTrue();
            shoppingList.Products.First().Price!.Amount.ShouldBe(20);
            shoppingList.Products.First().Price!.Currency.Value.ShouldBe("zł");
            shoppingList.Products.Last().IsBought.ShouldBeTrue();
            shoppingList.Products.Last().Price!.Amount.ShouldBe(30);
            shoppingList.Products.Last().Price!.Currency.Value.ShouldBe("zł");
        }

        [Fact]
        public void ChangePriceOfProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(true);

            var exception = Record.Exception(() 
                => shoppingList.ChangePriceOfProduct(ShoppingListProvider.ProductName, new Money(10m, "zł")));
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void ChangePriceOfProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.ChangePriceOfProduct(ShoppingListProvider.ProductName, new Money(10m, "zł")));
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void ChangePriceOfProduct_Throws_ShoppingListProductWasNotBoughtException_When_Product_Is_Not_Bought()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var product = ShoppingListProvider.GetProduct();
            shoppingList.AddProduct(product);
            var exception = Record.Exception(() 
                => shoppingList.ChangePriceOfProduct(ShoppingListProvider.ProductName, new Money(10m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductWasNotBoughtException>();
        }

        [Fact]
        public void ChangePriceOfProduct_Adds_ShoppingListProductPriceChanged_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct();

            shoppingList.AddProduct(product);
            shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł"));
            shoppingList.ClearEvents();

            shoppingList.ChangePriceOfProduct(ShoppingListProvider.ProductName, new Money(25m, "zł"));

            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductPriceChanged;
            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe(ShoppingListProvider.ProductName);
            @event.Price.Amount.ShouldBe(25);
            @event.Price.Currency.Value.ShouldBe("zł");
        }

        [Fact]
        public void CancelPurchaseOfProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(true);

            var exception = Record.Exception(() 
                => shoppingList.CancelPurchaseOfProduct(ShoppingListProvider.ProductName));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void CancelPurchaseOfProduct_Throws_ShoppingListProductNotFoundException_When_Product_With_Given_Name_Was_Not_Found()
        {
            var shopping = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shopping.CancelPurchaseOfProduct(ShoppingListProvider.ProductName));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductNotFoundException>();
        }

        [Fact]
        public void CancelPurchaseOfProduct_Throws_ShoppingListProductWasNotBoughtException_When_Product_Is_Not_Bought()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct(isBought: false);
            shoppingList.AddProduct(product);

            var exception = Record.Exception(() 
                => shoppingList.CancelPurchaseOfProduct(ShoppingListProvider.ProductName));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductWasNotBoughtException>();
        }

        [Fact]
        public void CancelPurchaseOfProduct_Adds_ShoppingListProductPurchaseCanceled_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct(isBought: false);
            shoppingList.AddProduct(product);

            shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł"));
            shoppingList.ClearEvents();

            shoppingList.CancelPurchaseOfProduct(ShoppingListProvider.ProductName);

            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListProductPurchaseCanceled;

            @event.ShouldNotBeNull();
            @event.ProductName.ShouldBe(ShoppingListProvider.ProductName);
        }

        [Fact]
        public void MakeListDone_Throws_ShoppingListAlreadyDoneException_When_Is_Already_Done()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(isDone: true);

            var exception = Record.Exception(() => shoppingList.MakeListDone());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListAlreadyDoneException>();
        }

        [Fact]
        public void MakeListDone_Adds_ShoppingListDone_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            shoppingList.MakeListDone();

            shoppingList.IsDone.ShouldBeTrue();
            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListDone;
            @event.ShouldNotBeNull();
        }

        [Fact]
        public void UndoListDone_Adds_ShoppingListUndone_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(isDone: false);

            shoppingList.UndoListDone();
            shoppingList.IsDone.ShouldBeFalse();
            shoppingList.Events.Count().ShouldBe(1);
            var @event = shoppingList.Events.FirstOrDefault() as ShoppingListUndone;
            @event.ShouldNotBeNull();
        }

        [Fact]
        public void UpdateProduct_Update_ShoppingListProduct_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetWithProduct(netContent: new NetContent("500", NetContentType.g));

            var product = shoppingList.Products.First();

            var quantityBeforeChanged = product.Quantity.Value;

            shoppingList.UpdateProduct(new ShoppingListProduct("NewProductName", 2, netContent: new NetContent("300", NetContentType.g)), ShoppingListProvider.ProductName);

            quantityBeforeChanged.ShouldBe(1);

            product.Name.Value.ShouldBe("NewProductName");
            product.Quantity.Value.ShouldBe(2);
            product.NetContent!.Value.ShouldBe("300");
        }
    }
}
