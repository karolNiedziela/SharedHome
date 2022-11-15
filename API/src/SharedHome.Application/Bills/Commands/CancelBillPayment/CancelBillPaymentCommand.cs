using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.CancelBillPayment
{
    public class CancelBillPaymentCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid BillId { get; set; }
    }
}
