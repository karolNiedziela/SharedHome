using SharedHome.Application.Bills.DTO;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetBill : AuthorizeRequest, IQuery<Response<BillDto>>
    {
        public int Id { get; set; }
    }
}
