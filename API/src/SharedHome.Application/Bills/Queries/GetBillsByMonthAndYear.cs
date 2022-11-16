using SharedHome.Application.Bills.DTO;
using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetBillsByMonthAndYear : AuthorizeRequest, IRequest<Response<List<BillDto>>>
    {
        public int? Month { get; set; }

        public int? Year { get; set; }
    }
}
