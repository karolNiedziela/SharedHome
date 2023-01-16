using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.Common.Requests;
using MediatR;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetShoppingList : AuthorizeRequest, IRequest<ApiResponse<ShoppingListDto>>
    {
        public Guid Id { get; set; }
    }
}
