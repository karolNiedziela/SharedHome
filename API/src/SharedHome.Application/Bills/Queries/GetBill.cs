using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Common.Queries;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetBill : AuthorizeRequest, IQuery<Response<BillDto>>
    {
        public Guid Id { get; set; }
    }
}
