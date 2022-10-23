using MediatR;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.DeleteBill
{
    public class DeleteBillHandler : ICommandHandler<DeleteBillCommand, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public DeleteBillHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.BillId, request.PersonId);

            await _billRepository.DeleteAsync(bill);

            return Unit.Value;
        }
    }
}
