using MediatR;
using SharedHome.Application.Common.DTO;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.PayForBill
{
    public class PayForBillCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid BillId { get; set; }

        public MoneyDto Cost { get; set; } = default!;
    }
}
