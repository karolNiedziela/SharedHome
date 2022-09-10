using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands.ChangeBillCost
{
    public class ChangeBillCostCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int BillId { get; set; }

        public MoneyDto Cost { get; set; } = default!;
    }
}
