using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.ChangeBillCost
{
    public class ChangeBillCostCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid BillId { get; set; }

        public MoneyDto Cost { get; set; } = default!;
    }
}
