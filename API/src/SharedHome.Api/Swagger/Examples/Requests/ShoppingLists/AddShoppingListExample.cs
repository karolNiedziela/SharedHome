using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingList;
using Swashbuckle.AspNetCore.Filters;

namespace SharedHome.Api.Swagger.Examples.Requests.ShoppingLists
{
    public class AddShoppingListExample : IExamplesProvider<AddShoppingListCommand>
    {
        public AddShoppingListCommand GetExamples()
        {
            return new AddShoppingListCommand
            {
                Name = "Lidl"
            };
        }
    }
}
