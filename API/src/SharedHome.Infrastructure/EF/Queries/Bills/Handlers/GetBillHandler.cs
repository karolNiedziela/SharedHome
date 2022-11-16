using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Application.ReadServices;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    internal class GetBillHandler : IRequestHandler<GetBill, Response<BillDto>>
    {
        private readonly DbSet<BillReadModel> _bills;
        private readonly IMapper _mapper;
        private readonly IHouseGroupReadService _houseGroupService;

        public GetBillHandler(ReadSharedHomeDbContext context, IMapper mapper, IHouseGroupReadService houseGroupService)
        {
            _bills = context.Bills;
            _mapper = mapper;
            _houseGroupService = houseGroupService;
        }

        public async Task<Response<BillDto>> Handle(GetBill request, CancellationToken cancellationToken)
        {
            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                var houseGroupPersonIds = await _houseGroupService.GetMemberPersonIds(request.PersonId!);

                var billsFromHouseGroup = await _bills
                    .SingleOrDefaultAsync(bill => bill.Id == request.Id && houseGroupPersonIds.Contains(bill.PersonId));

                return new Response<BillDto>(_mapper.Map<BillDto>(billsFromHouseGroup!));
            }

            var bill = await _bills
                .SingleOrDefaultAsync(bill => bill.PersonId == request.PersonId && bill.Id == request.Id);

            return new Response<BillDto>(_mapper.Map<BillDto>(bill!));
        }
    }
}
