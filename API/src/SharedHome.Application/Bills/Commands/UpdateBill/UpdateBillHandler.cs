using MediatR;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;

using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.UpdateBill
{
    public class UpdateBillHandler : IRequestHandler<UpdateBillCommand, Unit>
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
            var bill = await _billService.GetAsync(request.BillId, request.PersonId!);

            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            var money = request.Cost == null ? null : new Money(request.Cost.Price, request.Cost.Currency);

            bill.Update(billType, request.ServiceProviderName, bill.DateOfPayment, money);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
