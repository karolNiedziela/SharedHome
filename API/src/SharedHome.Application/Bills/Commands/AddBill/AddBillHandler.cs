using MapsterMapper;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Events;
using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Common.Events;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.AddBill
{
    public class AddBillHandler : ICommandHandler<AddBillCommand, Response<BillDto>>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public AddBillHandler(IBillRepository billRepository, IMapper mapper, IDomainEventDispatcher eventDispatcher)
        {
            _billRepository = billRepository;
            _mapper = mapper;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Response<BillDto>> Handle(AddBillCommand request, CancellationToken cancellationToken)
        {
            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            var bill = Bill.Create(request.BillId, request.PersonId, billType, request.ServiceProviderName,
                DateOnly.FromDateTime(request.DateOfPayment));

            await _billRepository.AddAsync(bill);

            await _eventDispatcher.Dispatch(new BillCreated(
                request.BillId, 
                bill.ServiceProvider, 
                bill.DateOfPayment,
                new CreatorDto(request.PersonId!, request.FirstName!, request.LastName!)));

            return new Response<BillDto>(_mapper.Map<BillDto>(bill));
        }
    }
}
