using MediatR;
using SharedHome.Application.Common.DTO;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Bills.Commands.ChangeBillCost
{
    public class ChangeBillCostCommand : AuthorizeRequest, IRequest<Unit>
    {
        public Guid BillId { get; set; }

        public MoneyDto Cost { get; set; } = default!;
    }
}
