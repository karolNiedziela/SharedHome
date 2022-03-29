using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands
{
    public class CancelBillPayment : AuthorizeCommand, ICommand<Unit>
    {
        public int BillId { get; set; }
    }
}
