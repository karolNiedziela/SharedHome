using MediatR;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class PayForBillHandler : ICommandHandler<PayForBill, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public PayForBillHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(PayForBill request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.BillId, request.PersonId!);

            var money = new Money(request.Cost, request.Currency);

            bill.PayFor(money);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
