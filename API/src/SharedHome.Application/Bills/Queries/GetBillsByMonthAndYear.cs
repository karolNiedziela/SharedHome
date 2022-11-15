using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Common.Queries;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetBillsByMonthAndYear : AuthorizeRequest, IQuery<Response<List<BillDto>>>
    {
        public int? Month { get; set; }

        public int? Year { get; set; }
    }
}
