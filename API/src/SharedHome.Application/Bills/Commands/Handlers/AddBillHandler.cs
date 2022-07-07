using MediatR;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Helpers;

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
            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            var money = new Money(request.Cost, request.Currency);

            var bill = Bill.Create(request.PersonId!, billType, request.ServiceProviderName,
                request.DateOfPayment, money);

            await _billRepository.AddAsync(bill);

            return Unit.Value;
        }
    }
}
