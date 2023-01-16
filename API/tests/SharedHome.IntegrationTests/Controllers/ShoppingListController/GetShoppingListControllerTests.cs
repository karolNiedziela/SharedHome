using SharedHome.Api.Constants;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Shared.Application.Responses;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class GetShoppingListControllerTests : ShoppingListControllerTestsBase
    {
        public GetShoppingListControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_Should_Return_200_Status_Code()
        {
            var shoppingList = ShoppingListProvider.GetEmpty(personId: TestDbInitializer.PersonId);

            await _shoppingListSeed.AddAsync(shoppingList);

            Authorize(shoppingList.PersonId);

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.ShoppingLists.Get.Replace("{shoppingListId}", shoppingList.Id.Value.ToString())}";
            var response = await Client.GetAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var shoppingListDto = await response.Content.ReadFromJsonAsync<ApiResponse<ShoppingListDto>>();
            shoppingListDto?.Data.Name.ShouldBe("ShoppingList");
        }
    }
}
