using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.ChangeBillDateOfPayment
{
    public class ChangeBillDateOfPaymentCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid BillId { get; set; }

        public DateTime DateOfPayment { get; set; }

    }
}
