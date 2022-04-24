using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands
{
    public class DeleteBill : AuthorizeRequest, ICommand<Unit>
    {
        public int BillId { get; set; }
    }
}
