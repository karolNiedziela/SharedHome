using MediatR;
using SharedHome.Application.Common.DTO;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.UpdateBill
{
    public class UpdateBillCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid BillId { get; set; }

        public int BillType { get; set; }

        public string ServiceProviderName { get; set; } = default!;

        public DateTime DateOfPayment { get; set; }

        public MoneyDto? Cost { get; set; }
    }
}
