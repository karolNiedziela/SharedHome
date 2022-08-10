using MediatR;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.UpdateBill
{
    public class UpdateBillHandler : ICommandHandler<UpdateBillCommand, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public UpdateBillHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.Id, request.PersonId!);

            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            var money = new Money(request.Cost, request.Currency);

            bill.Update(billType, request.ServiceProviderName, request.DateOfPayment, money);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
