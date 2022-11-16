using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.CancelBillPayment
{
    public class CancelBillPaymentCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid BillId { get; set; }
    }
}
