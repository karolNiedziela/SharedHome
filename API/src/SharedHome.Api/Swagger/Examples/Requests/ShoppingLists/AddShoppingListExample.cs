using SharedHome.Application.ShoppingLists.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace SharedHome.Api.Swagger.Examples.Requests.ShoppingLists
{
    public class AddShoppingListExample : IExamplesProvider<AddShoppingList>
    {
        public AddShoppingList GetExamples()
        {
            return new AddShoppingList
            {
                Name = "Lidl"
            };
        }
    }
}
