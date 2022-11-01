using SharedHome.Domain.Shared.Exceptions;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
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
                => ShoppingList.Create(Guid.NewGuid(), shoppingListName, ShoppingListProvider.PersonId));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyShoppingListNameException>();
        }

        [Fact]
        public void Create_Throws_TooLongShoppingListNameException_When_ShoppingListName_Is_Longer_Than_20_Characters()
        {
            var exception = Record.Exception(() 
                => ShoppingList.Create(Guid.NewGuid(), new string('k', 25), ShoppingListProvider.PersonId));

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
                => shoppingList.AddProduct(ShoppingListProduct.Create(shoppingListProductName, 1)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyShoppingListProductNameException>();
        }

        [Fact]
        public void AddProduct_Throws_QuantityBelowZeroException_When_Quantity_Is_Lower_Than_Zero()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(ShoppingListProduct.Create("Product", -5)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<QuantityBelowZeroException>();
        }

        [Fact]
        public void AddProduct_Throws_MoneyAmountBelowZeroException_When_ProductPrice_Is_Lower_Than_Zero()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(ShoppingListProduct.Create("Product", 2, new Money(-10m, "zł"))));

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
                => shoppingList.AddProduct(ShoppingListProduct.Create("Product", 2, new Money(10m, currency))));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidCurrencyException>();
        }

        [Fact]
        public void Add_Product_Throws_UnsupportedCurrencyException_When_Currency_Is_Not_In_AllowedValues()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var exception = Record.Exception(()
                => shoppingList.AddProduct(ShoppingListProduct.Create("Product", 2, new Money(10m, "GBR"))));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<UnsupportedCurrencyException>();
        }     

        [Fact]
        public void AddProduct_Throws_ShoppingListAlreadyDoneException_When_Shopping_List_Is_Marked_As_Done()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(true);

            var exception = Record.Exception(() 
                => shoppingList.AddProduct(ShoppingListProduct.Create(ShoppingListProvider.ProductName, 1)));

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
        public void AddProduct_Should_Add_Product_To_ShoppingList()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct();

            shoppingList.AddProduct(product);

            shoppingList.Products.Count.ShouldBe(1);
            shoppingList.Products[0].Name.Value.ShouldBe("Product");
            shoppingList.Products[0].Quantity.Value.ShouldBe(1);
        }

        [Fact]
        public void AddProducts_Adds_ShoppingListProductAdded_On_Success()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var products = new List<ShoppingListProduct>
            {
                ShoppingListProduct.Create("Product 1", 1),
                ShoppingListProduct.Create("Product 2", 2),
            };

            shoppingList.AddProducts(products);

            shoppingList.Products.Count.ShouldBe(2);
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
        public void RemoveProduct_Should_Remove_Product_From_ShoppingList()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct();
            shoppingList.AddProduct(product);

            shoppingList.RemoveProduct(ShoppingListProvider.ProductName);

            shoppingList.Products.Count.ShouldBe(0);
        }

        [Fact]
        public void RemoveProducts_Remove_Products_With_Given_ProductNames()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            shoppingList.AddProduct(ShoppingListProduct.Create("Produkt1", 1));
            shoppingList.AddProduct(ShoppingListProduct.Create("Produkt2", 1));
            shoppingList.AddProduct(ShoppingListProduct.Create("Produkt3", 1));

            shoppingList.RemoveProducts(new[] { "Produkt1", "Produkt3" });

            shoppingList.Products.Count.ShouldBe(1);
            shoppingList.Products[0].Name.Value.ShouldBe("Produkt2");
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

            var exception = Record.Exception(() 
                => shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł")));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListProductIsAlreadyBoughtException>();
        }

        [Fact]
        public void PurchaseProduct_Should_Change_IsBought_To_True()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            var product = ShoppingListProvider.GetProduct();
            shoppingList.AddProduct(product);

            shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł"));

            shoppingList.Products[0].IsBought.ShouldBeTrue();
            shoppingList.Products[0].Price!.Amount.ShouldBe(10m);
            shoppingList.Products[0].Price!.Currency.Value.ShouldBe("zł");
        }

        [Fact]
        public void PurchaseProducts_Set_Price_And_Is_Bought_To_True()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            shoppingList.AddProducts(new[]
            {
                ShoppingListProduct.Create("Product", 1),
                ShoppingListProduct.Create("Product2", 1),
            });

            shoppingList.PurchaseProducts(new Dictionary<string, Money>
            {
                { "Product", new Money(20, "zł") },
                { "Product2", new Money(30, "zł") },
            });

            shoppingList.Products[0].IsBought.ShouldBeTrue();
            shoppingList.Products[0].Price!.Amount.ShouldBe(20);
            shoppingList.Products[0].Price!.Currency.Value.ShouldBe("zł");
            shoppingList.Products[1].IsBought.ShouldBeTrue();
            shoppingList.Products[1].Price!.Amount.ShouldBe(30);
            shoppingList.Products[1].Price!.Currency.Value.ShouldBe("zł");
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
        public void ChangePriceOfProduct_Should_Change_Price()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct();

            shoppingList.AddProduct(product);
            shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł"));

            shoppingList.ChangePriceOfProduct(ShoppingListProvider.ProductName, new Money(25m, "zł"));
            
            shoppingList.Products[0].Price!.Amount.ShouldBe(25m);
            shoppingList.Products[0].Price!.Currency.Value.ShouldBe("zł");
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
        public void CancelPurchaseOfProduct_Should_Change_IsBought_To_False_And_Clear_Price()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            var product = ShoppingListProvider.GetProduct(isBought: false);
            shoppingList.AddProduct(product);

            shoppingList.PurchaseProduct(ShoppingListProvider.ProductName, new Money(10m, "zł"));            

            shoppingList.CancelPurchaseOfProduct(ShoppingListProvider.ProductName);

            shoppingList.Products[0].Price.ShouldBeNull();
            shoppingList.Products[0].IsBought.ShouldBeFalse();
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
        public void MakeListDone_Should_Set_IsDone_To_True()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            shoppingList.MakeListDone();

            shoppingList.IsDone.ShouldBeTrue();           
        }

        [Fact]
        public void UndoListDone_Should_Set_IsDone_To_False()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(isDone: false);

            shoppingList.UndoListDone();

            shoppingList.IsDone.ShouldBeFalse();
        }      
    }
}
