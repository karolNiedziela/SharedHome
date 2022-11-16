using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteManyShoppingListProducts
{
    public class DeleteManyShoppingListProductsCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public IEnumerable<string> ProductNames { get; set; } = new List<string>();
    }
}
