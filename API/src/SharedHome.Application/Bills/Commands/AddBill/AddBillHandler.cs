using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application.Bills.DTO;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Events;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.AddBill
{
    public class AddBillHandler : IRequestHandler<AddBillCommand, ApiResponse<BillDto>>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public AddBillHandler(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BillDto>> Handle(AddBillCommand request, CancellationToken cancellationToken)
        {
            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            var bill = Bill.Create(Guid.NewGuid(), request.PersonId, billType, request.ServiceProviderName,
            DateOnly.FromDateTime(request.DateOfPayment));
            await _billRepository.AddAsync(bill);

            return new ApiResponse<BillDto>(_mapper.Map<BillDto>(bill));
        }
    }
}
