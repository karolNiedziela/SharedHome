using MediatR;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class ChangeBillDateOfPaymentHandler : ICommandHandler<ChangeBillDateOfPayment, Unit>
    {
        private readonly IBillRepository _billRepository;

        public ChangeBillDateOfPaymentHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<Unit> Handle(ChangeBillDateOfPayment request, CancellationToken cancellationToken)
        {
            var bill = await _billRepository.GetOrThrowAsync(request.BillId, request.PersonId!);

            bill.ChangeDateOfPayment(DateOnly.FromDateTime(request.DateOfPayment));

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
