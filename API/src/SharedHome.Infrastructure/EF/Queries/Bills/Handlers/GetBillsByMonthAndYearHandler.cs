using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Application.ReadServices;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Time;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    internal class GetBillsByMonthAndYearHandler : IRequestHandler<GetBillsByMonthAndYear, Response<List<BillDto>>>
    {
        private readonly DbSet<BillReadModel> _bills;
        private readonly IMapper _mapper;
        private readonly ITimeProvider _time;
        private readonly IHouseGroupReadService _houseGroupService;

        public GetBillsByMonthAndYearHandler(ReadSharedHomeDbContext context, IMapper mapper, ITimeProvider time, IHouseGroupReadService houseGroupService)
        {
            _bills = context.Bills;
            _mapper = mapper;
            _time = time;
            _houseGroupService = houseGroupService;
        }

        public async Task<Response<List<BillDto>>> Handle(GetBillsByMonthAndYear request, CancellationToken cancellationToken)
        {
            if (!request.Year.HasValue || !request.Month.HasValue)
            {
                var currentDate = _time.CurrentDate();
                request.Year = currentDate.Year;
                request.Month = currentDate.Month;
            }

            if (await _houseGroupService.IsPersonInHouseGroupAsync(request.PersonId!))
            {
                var houseGroupPersonsId = await _houseGroupService.GetMemberPersonIdsAsync(request.PersonId!);

                var billsFromHouseGroup = await _bills
                    .Where(bill => houseGroupPersonsId.Contains(bill.PersonId) &&
                    bill.DateOfPayment.Month == request.Month &&
                    bill.DateOfPayment.Year == request.Year)
                    .Select(bill => _mapper.Map<BillDto>(bill))
                    .ToListAsync();

                return new Response<List<BillDto>>(billsFromHouseGroup);

            }

            var bills = await _bills
                .Where(bill =>
                    bill.DateOfPayment.Month == request.Month &&
                    bill.DateOfPayment.Year == request.Year)
                .Select(bill => _mapper.Map<BillDto>(bill))
                .ToListAsync();

            return new Response<List<BillDto>>(bills);
        }
    }
}
