using MapsterMapper;
using SharedHome.Application.Bills.DTO;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.AddBill
{
    public class AddBillHandler : ICommandHandler<AddBillCommand, Response<BillDto>>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public AddBillHandler(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        public async Task<Response<BillDto>> Handle(AddBillCommand request, CancellationToken cancellationToken)
        {
            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            var bill = Bill.Create(request.PersonId!, billType, request.ServiceProviderName,
                request.DateOfPayment);

            await _billRepository.AddAsync(bill);

            return new Response<BillDto>(_mapper.Map<BillDto>(bill));
        }
    }
}
