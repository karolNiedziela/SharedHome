using MediatR;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class DeleteBillHandler : ICommandHandler<DeleteBill, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public DeleteBillHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(DeleteBill request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.BillId, request.PersonId!);

            await _billRepository.DeleteAsync(bill);

            return Unit.Value;
        }
    }
}
