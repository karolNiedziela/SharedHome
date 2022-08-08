using SharedHome.Api.Constants;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingList;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts;
using SharedHome.Application.ShoppingLists.Commands.CancelPurchaseOfProduct;
using SharedHome.Application.ShoppingLists.Commands.ChangePriceOfProduct;
using SharedHome.Application.ShoppingLists.Commands.PurchaseProduct;
using SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone;
using SharedHome.Application.ShoppingLists.Commands.UpdateShoppingList;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.IntegrationTests.Extensions;
using SharedHome.IntegrationTests.Fixtures;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers
{
    public class ShoppingListsControllerTests : ControllerTests, IClassFixture<DatabaseFixture>, IDisposable
    {
        private const string BaseAddress = "api/shoppinglists";

        public DatabaseFixture Fixture { get; set; }

        public ShoppingListsControllerTests(SettingsProvider settingsProvider, DatabaseFixture fixture) : base(settingsProvider)
        {
            Fixture = fixture;    
        }

        [Fact]
        public async Task Get_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            ProviderNew(shoppingList);

            Authorize(shoppingList.PersonId);

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.Get.Replace("{shoppingListId:int}", shoppingList.Id.ToString())}";
            var response = await Client.GetAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var shoppingListDto = await response.Content.ReadFromJsonAsync<Response<ShoppingListDto>>();
            shoppingListDto?.Data.Name.ShouldBe("ShoppingList");
        }

        [Fact]
        public async Task Post_Should_Return_Created_201_Status_Code()
        {
            Authorize(userId: ShoppingListProvider.PersonId);

            var command = new AddShoppingListCommand
            {
                Name = "ShoppingList"
            };

            var response = await Client.PostAsJsonAsync(BaseAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Put_AddShoppingListProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var command = new AddShoppingListProductsCommand
            {
                ShoppingListId = shoppingList.Id,
                Products = new List<AddShoppingListProductDto>
                {
                    new AddShoppingListProductDto
                    {
                        ProductName = "Product",
                        Quantity = 2,
                    }
                }
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.AddShoppingListProduct.Replace("{shoppingListId:int}", shoppingList.Id.ToString())}";
            var response = await Client.PutAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Patch_PurchaseShoppingListProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetWithProduct();
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var command = new PurchaseProductCommand
            {
                Price = 100,
                ProductName = "Product",
                ShoppingListId = shoppingList.Id,
                Currency = "zł"
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.PurchaseShoppingList.Replace("{shoppingListId:int}", shoppingList.Id.ToString()).Replace("{productName}", "Product")}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Patch_CancePurchaseOfProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetWithProduct(price: new Money(100m, "zł"), isBought: true);
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var command = new CancelPurchaseOfProductCommand
            {
                ProductName = "Product",
                ShoppingListId = shoppingList.Id
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.CancelPurchaseOfProduct.Replace("{shoppingListId:int}", shoppingList.Id.ToString()).Replace("{productName}", "Product")}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Patch_ChangePriceOfProduct_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetWithProduct(price: new Money(100m, "zł"), isBought: true);
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var command = new ChangePriceOfProductCommand
            {
                Price = 100,
                ProductName = "Product",
                ShoppingListId = shoppingList.Id,
                Currency = "zł"
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.ChangePriceOfProduct.Replace("{shoppingListId:int}", shoppingList.Id.ToString()).Replace("{productName}", "Product")}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Patch_SetIsDone_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var command = new SetIsShoppingListDoneCommand
            {
                IsDone = true,
                ShoppingListId = shoppingList.Id
            };

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.SetIsDone.Replace("{shoppingListId:int}", shoppingList.Id.ToString())}";

            var response = await Client.PatchAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Put_Update_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var command = new UpdateShoppingListCommand
            {
                Id = shoppingList.Id,
                Name = "NewShoppingList"
            };

            var endpointAddress = $"{BaseAddress}";

            var response = await Client.PutAsJsonAsync(endpointAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_Should_Return_204_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.Delete.Replace("{shoppingListId:int}", shoppingList.Id.ToString())}";

            var response = await Client.DeleteAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ShoppingListProduct_Should_Return_204_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetWithProduct();
            ProviderNew(shoppingList);

            Authorize(userId: shoppingList.PersonId);

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.Delete.Replace("{shoppingListId:int}", shoppingList.Id.ToString()).Replace("{productName}", ShoppingListProvider.ProductName)}";

            var response = await Client.DeleteAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        private void ProviderNew(ShoppingList shoppingList)
        {
            Fixture.WriteContext.ShoppingLists.Add(shoppingList);
            Fixture.WriteContext.SaveChanges();
        }

        public void Dispose()
        {
            var shoppingList = Fixture.WriteContext.ShoppingLists.FirstOrDefault(x => x.Name.Name == ShoppingListProvider.ShoppingListName);
            if (shoppingList is null)
            {
                return;
            }

            Fixture.WriteContext.ShoppingLists.Remove(shoppingList);
            Fixture.WriteContext.SaveChanges();

            GC.SuppressFinalize(this);
        }
    }
}
