using MediatR;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class ChangeBillCostHandler : ICommandHandler<ChangeBillCost, Unit>
    {
        private readonly IBillRepository _billRepository;

        public ChangeBillCostHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<Unit> Handle(ChangeBillCost request, CancellationToken cancellationToken)
        {
            var bill = await _billRepository.GetOrThrowAsync(request.BillId, request.PersonId!);

            bill.ChangeCost(request.Cost);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
