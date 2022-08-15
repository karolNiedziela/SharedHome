using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteManyShoppingListProducts
{
    public class DeleteManyShoppingListProductsCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int ShoppingListId { get; set; }

        public IEnumerable<string> ProductNames { get; set; } = new List<string>();
    }
}
