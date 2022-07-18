using AutoMapper;
using MediatR;
using SharedHome.Application.Bills.DTO;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.Handlers
{
    public class AddBillHandler : ICommandHandler<AddBill, Response<BillDto>>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public AddBillHandler(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        public async Task<Response<BillDto>> Handle(AddBill request, CancellationToken cancellationToken)
        {
            var billType = EnumHelper.ToEnumByIntOrThrow<BillType>(request.BillType);

            var money = request.Cost.HasValue && !string.IsNullOrEmpty(request.Currency) ?
                new Money(request.Cost.Value, request.Currency) :
                null;

            var bill = Bill.Create(request.PersonId!, billType, request.ServiceProviderName,
                request.DateOfPayment, money);

            await _billRepository.AddAsync(bill);

            return new Response<BillDto>(_mapper.Map<BillDto>(bill));
        }
    }
}
