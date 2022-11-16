using MediatR;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;


namespace SharedHome.Application.Bills.Commands.PayForBill
{
    public class PayForBillHandler : IRequestHandler<PayForBillCommand, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public PayForBillHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(PayForBillCommand request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.BillId, request.PersonId);

            var money = new Money(request.Cost.Price, request.Cost.Currency);

            bill.PayFor(money);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
