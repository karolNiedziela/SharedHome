using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using System.Windows.Input;

namespace SharedHome.Application.Bills.Commands
{
    public class ChangeBillDateOfPayment : AuthorizeCommand, ICommand<Unit>
    {
        public int BillId { get; set; }

        public DateTime DateOfPayment { get; set; }
    }
}
