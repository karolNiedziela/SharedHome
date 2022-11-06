using SharedHome.Application.ShoppingLists.Commands.AddShoppingList;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.ShoppingListController
{
    public class CreateShoppingListControllerTests : ShoppingListControllerTestsBase
    {
        public CreateShoppingListControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Post_Should_Return_Created_201_Status_Code()
        {
            Authorize(userId: TestDbInitializer.PersonId);

            var command = new AddShoppingListCommand
            {
                Name = "ShoppingList"
            };

            var response = await Client.PostAsJsonAsync(BaseAddress, command);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

    }
}
