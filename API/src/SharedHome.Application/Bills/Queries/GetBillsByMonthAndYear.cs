using SharedHome.Application.Bills.DTO;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetBillsByMonthAndYear : IAuthorizeRequest, IQuery<Response<List<BillDto>>>
    {
        public int? Month { get; set; }

        public int? Year { get; set; }

        public bool IsPaid { get; set; } = false;

        public string? PersonId { get; set; }
    }
}
