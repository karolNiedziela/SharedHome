using MediatR;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class AddBillHandler : ICommandHandler<AddBill, Unit>
    {
        private readonly IBillRepository _billRepository;

        public AddBillHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<Unit> Handle(AddBill request, CancellationToken cancellationToken)
        {
            var billType = (BillType)Enum.Parse(typeof(BillType), request.BillType);

            var bill = Bill.Create(request.PersonId!, billType, request.ServiceProviderName,
                DateOnly.FromDateTime(request.DateOfPayment), request.Cost);

            await _billRepository.AddAsync(bill);

            return Unit.Value;
        }
    }
}
