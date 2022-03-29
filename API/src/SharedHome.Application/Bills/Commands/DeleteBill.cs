using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands
{
    public class DeleteBill : AuthorizeCommand, ICommand<Unit>
    {
        public int BillId { get; set; }
    }
}
