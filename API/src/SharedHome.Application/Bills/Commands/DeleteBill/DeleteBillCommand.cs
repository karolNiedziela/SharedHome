using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.DeleteBill
{
    public class DeleteBillCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid BillId { get; set; }
    }
}
