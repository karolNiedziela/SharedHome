using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.DeleteBill
{
    public class DeleteBillCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid BillId { get; set; }
    }
}
