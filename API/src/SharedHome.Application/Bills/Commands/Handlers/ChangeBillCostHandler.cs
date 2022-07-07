using MediatR;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class ChangeBillCostHandler : ICommandHandler<ChangeBillCost, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public ChangeBillCostHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(ChangeBillCost request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.BillId, request.PersonId!);

            var money = new Money(request.Cost, request.Currency);

            bill.ChangeCost(money);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
