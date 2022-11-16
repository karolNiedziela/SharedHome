using MediatR;
using SharedHome.Application.ShoppingLists.DTO;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts
{
    public class AddShoppingListProductsCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public IReadOnlyList<AddShoppingListProductDto> Products { get; set; } = new List<AddShoppingListProductDto>();
    }
}
