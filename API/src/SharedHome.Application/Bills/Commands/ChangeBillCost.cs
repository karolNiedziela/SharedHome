using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands
{
    public class ChangeBillCost : AuthorizeCommand, ICommand<Unit>
    {
        public int BillId { get; set; }

        public decimal Cost { get; set; }
    }
}
