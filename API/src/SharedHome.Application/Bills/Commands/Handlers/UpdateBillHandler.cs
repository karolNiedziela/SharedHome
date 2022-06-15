using MediatR;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class UpdateBillHandler : ICommandHandler<UpdateBill, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public UpdateBillHandler(IBillRepository billRepository, IBillService billService)
        {
            _billRepository = billRepository;
            _billService = billService;
        }

        public async Task<Unit> Handle(UpdateBill request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetAsync(request.Id, request.PersonId!);

            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            bill.Update(billType, request.ServiceProviderName, request.DateOfPayment, request.Cost);

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
