using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    internal class GetBillHandler : IRequestHandler<GetBill, ApiResponse<BillDto>>
    {
        private readonly DbSet<BillReadModel> _bills;
        private readonly IMapper _mapper;
        private readonly IHouseGroupRepository _houseGroupRepository;

        public GetBillHandler(ReadSharedHomeDbContext context, IMapper mapper, IHouseGroupRepository houseGroupRepository)
        {
            _bills = context.Bills;
            _mapper = mapper;
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<ApiResponse<BillDto>> Handle(GetBill request, CancellationToken cancellationToken)
        {
            if (await _houseGroupRepository.IsPersonInHouseGroupAsync(request.PersonId!))
            {
                var houseGroupPersonIds = await _houseGroupRepository.GetMemberPersonIdsAsync(request.PersonId!);

                var billsFromHouseGroup = await _bills
                    .SingleOrDefaultAsync(bill => bill.Id == request.Id && 
                    houseGroupPersonIds
                    .Contains(bill.PersonId));

                return new ApiResponse<BillDto>(_mapper.Map<BillDto>(billsFromHouseGroup!));
            }

            var bill = await _bills
                .SingleOrDefaultAsync(bill => bill.PersonId == request.PersonId && bill.Id == request.Id);

            return new ApiResponse<BillDto>(_mapper.Map<BillDto>(bill!));
        }
    }
}
