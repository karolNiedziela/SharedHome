using MediatR;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class CancelBillPaymentHandler : ICommandHandler<CancelBillPayment, Unit>
    {
        private readonly IBillRepository _billRepository;

        public CancelBillPaymentHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<Unit> Handle(CancelBillPayment request, CancellationToken cancellationToken)
        {
            var bill = await _billRepository.GetOrThrowAsync(request.BillId, request.PersonId!);

            bill.CancelPayment();

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
