using MediatR;
using SharedHome.Application.Bills.Exceptions;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class PayForBillHandler : ICommandHandler<PayForBill, Unit>
    {
        private readonly IBillRepository _billRepository;

        public PayForBillHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<Unit> Handle(PayForBill request, CancellationToken cancellationToken)
        {
            var bill = await _billRepository.GetOrThrowAsync(request.BillId, request.PersonId!);

            bill.PayFor(request.Cost);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
