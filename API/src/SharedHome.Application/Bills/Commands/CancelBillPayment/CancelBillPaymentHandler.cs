using MediatR;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;


namespace SharedHome.Application.Bills.Commands.CancelBillPayment
{
    public class CancelBillPaymentHandler : IRequestHandler<CancelBillPaymentCommand, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public CancelBillPaymentHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(CancelBillPaymentCommand request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.BillId, request.PersonId);

            bill.CancelPayment();

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
