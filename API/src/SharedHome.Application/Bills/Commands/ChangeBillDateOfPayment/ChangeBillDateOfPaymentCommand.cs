using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands.ChangeBillDateOfPayment
{
    public class ChangeBillDateOfPaymentCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid BillId { get; set; }

        public DateTime DateOfPayment { get; set; }

    }
}
