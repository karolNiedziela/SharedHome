using MediatR;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class DeleteBillHandler : ICommandHandler<DeleteBill, Unit>
    {
        private readonly IBillRepository _billRepository;

        public DeleteBillHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<Unit> Handle(DeleteBill request, CancellationToken cancellationToken)
        {
            var bill = await _billRepository.GetOrThrowAsync(request.BillId, request.PersonId!);

            await _billRepository.DeleteAsync(bill);

            return Unit.Value;
        }
    }
}
