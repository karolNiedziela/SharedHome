using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands
{
    public class DeleteBill : IAuthorizeRequest, ICommand<Unit>
    {
        public int BillId { get; set; }

        public string? PersonId { get; set; }
    }
}
