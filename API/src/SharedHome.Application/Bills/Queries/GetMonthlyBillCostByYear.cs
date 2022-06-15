using SharedHome.Application.Bills.DTO;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetMonthlyBillCostByYear : AuthorizeRequest, IQuery<Response<List<BillMonthlyCostDto>>>
    {
        public int? Year { get; set; }
    }
}
