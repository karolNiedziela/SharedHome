using SharedHome.Application.Bills.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Bills.Commands.AddBill
{
    public class AddBillCommand : AuthorizeRequest, ICommand<Response<BillDto>>
    {
        public Guid BillId { get; init; } = Guid.NewGuid();

        public int BillType { get; set; }

        public string ServiceProviderName { get; set; } = default!;

        public DateTime DateOfPayment { get; set; }
    }
}
