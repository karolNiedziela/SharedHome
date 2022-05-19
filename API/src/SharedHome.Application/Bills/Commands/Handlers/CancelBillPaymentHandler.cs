using MediatR;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.Services;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class CancelBillPaymentHandler : ICommandHandler<CancelBillPayment, Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IBillService _billService;

        public CancelBillPaymentHandler(IBillRepository billRepository, IHouseGroupReadService houseGroupService, IBillService billService)
        {
            _billRepository = billRepository;
            _houseGroupService = houseGroupService;
            _billService = billService;
        }

        public async Task<Unit> Handle(CancelBillPayment request, CancellationToken cancellationToken)
        {
            var bill = await _houseGroupService.IsPersonInHouseGroup(request.PersonId!) ?
                await _billService.GetForHouseGroupMemberAsync(request.BillId, request.PersonId!) :
                await _billRepository.GetOrThrowAsync(request.BillId, request.PersonId!);

            bill.CancelPayment();

            await _billRepository.UpdateAsync(bill);

            return Unit.Value;
        }
    }
}
