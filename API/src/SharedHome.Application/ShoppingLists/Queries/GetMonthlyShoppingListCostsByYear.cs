using SharedHome.Application.DTO;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Queries
{
    public class GetMonthlyShoppingListCostsByYear : AuthorizeRequest, IQuery<Response<List<ShoppingListMonthlyCostDto>>>
    {
        public int? Year { get; set; }
    }
}
