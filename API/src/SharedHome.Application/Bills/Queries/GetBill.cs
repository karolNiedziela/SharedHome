using SharedHome.Application.Bills.DTO;
using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Bills.Queries
{
    public class GetBill : AuthorizeRequest, IRequest<ApiResponse<BillDto>>
    {
        public Guid Id { get; set; }
    }
}
