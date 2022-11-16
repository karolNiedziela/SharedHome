using SharedHome.Application.Bills.DTO;
using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetMonthlyBillCostByYear : AuthorizeRequest, IRequest<Response<List<BillMonthlyCostDto>>>
    {
        public int? Year { get; set; }
    }
}
