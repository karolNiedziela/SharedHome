using MediatR;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class ChangeBillDateOfPaymentHandler : ICommandHandler<ChangeBillDateOfPayment, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public ChangeBillDateOfPaymentHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(ChangeBillDateOfPayment request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.BillId, request.PersonId!);

            bill.ChangeDateOfPayment(request.DateOfPayment);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
