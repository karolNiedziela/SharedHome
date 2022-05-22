using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands
{
    public class ChangeBillDateOfPayment : IAuthorizeRequest, ICommand<Unit>
    {
        public int BillId { get; set; }

        public DateTime DateOfPayment { get; set; }

        public string? PersonId { get; set; }
    }
}
