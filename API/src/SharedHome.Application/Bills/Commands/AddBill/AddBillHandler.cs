﻿using MapsterMapper;
using MediatR;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Events;
using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Common.Events;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.Bills.Commands.AddBill
{
    public class AddBillHandler : IRequestHandler<AddBillCommand, Response<BillDto>>
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

            var bill = Bill.Create(Guid.NewGuid(), request.PersonId, billType, request.ServiceProviderName,
                DateOnly.FromDateTime(request.DateOfPayment));

            await _billRepository.AddAsync(bill);

            await _eventDispatcher.DispatchAsync(new BillCreated(
                bill.Id, 
                bill.ServiceProvider, 
                bill.DateOfPayment,
                new CreatorDto(request.PersonId!, request.FirstName!, request.LastName!)));

            return new Response<BillDto>(_mapper.Map<BillDto>(bill));
        }
    }
}
