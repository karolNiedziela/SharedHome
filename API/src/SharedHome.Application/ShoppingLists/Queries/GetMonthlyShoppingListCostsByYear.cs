using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.Common.Queries;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetMonthlyShoppingListCostsByYear : AuthorizeRequest, IQuery<Response<List<ShoppingListMonthlyCostDto>>>
    {
        public int? Year { get; set; }
    }
}
