using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands.UpdateBill
{
    public class UpdateBillCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid BillId { get; set; }

        public int BillType { get; set; }

        public string ServiceProviderName { get; set; } = default!;

        public DateTime DateOfPayment { get; set; }

        public MoneyDto? Cost { get; set; }
    }
}
