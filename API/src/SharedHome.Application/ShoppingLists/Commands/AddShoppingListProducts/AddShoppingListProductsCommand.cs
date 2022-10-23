using MediatR;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts
{
    public class AddShoppingListProductsCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid ShoppingListId { get; set; }

        public IReadOnlyList<AddShoppingListProductDto> Products { get; set; } = new List<AddShoppingListProductDto>();
    }
}
