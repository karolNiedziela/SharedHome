using MediatR;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Bills.Commands.AddBill
{
    public class AddBillCommand : AuthorizeRequest, IRequest<ApiResponse<BillDto>>
    {
        public int BillType { get; set; }

        public string ServiceProviderName { get; set; } = default!;

        public DateTime DateOfPayment { get; set; }
    }
}
