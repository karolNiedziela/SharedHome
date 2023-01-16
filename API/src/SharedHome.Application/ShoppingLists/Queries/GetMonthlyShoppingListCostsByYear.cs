using SharedHome.Application.ShoppingLists.DTO;
using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetMonthlyShoppingListCostsByYear : AuthorizeRequest, IRequest<ApiResponse<List<ShoppingListMonthlyCostDto>>>
    {
        public int? Year { get; set; }
    }
}
