using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;
using MediatR;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingList
{
    public class AddShoppingListCommand : AuthorizeRequest, IRequest<ApiResponse<ShoppingListDto>>
    {
        public string Name { get; set; } = default!;

        public IReadOnlyList<AddShoppingListProductDto> Products { get; set; } = new List<AddShoppingListProductDto>();
    }
}
