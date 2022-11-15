using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteManyShoppingListProducts
{
    public class DeleteManyShoppingListProductsCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public IEnumerable<string> ProductNames { get; set; } = new List<string>();
    }
}
