using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Application.Services;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Abstractions.Time;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    internal class GetBillsByMonthAndYearHandler : IQueryHandler<GetBillsByMonthAndYear, Response<List<BillDto>>>
    {
        private readonly DbSet<BillReadModel> _bills;
        private readonly IMapper _mapper;
        private readonly ITime _time;
        private readonly IHouseGroupService _houseGroupService;

        public GetBillsByMonthAndYearHandler(ReadSharedHomeDbContext context, IMapper mapper, ITime time, IHouseGroupService houseGroupService)
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

            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                var houseGroupPersonsId = await _houseGroupService.GetHouseGroupPersonsId(request.PersonId!);

                var billsFromHouseGroup = await _bills
                    .Where(bill => houseGroupPersonsId.Contains(bill.PersonId) &&
                    bill.IsPaid == request.IsPaid &&
                    bill.DateOfPayment.Month == request.Month &&
                    bill.DateOfPayment.Year == request.Year)
                    .Select(bill => _mapper.Map<BillDto>(bill))
                    .ToListAsync();

                return new Response<List<BillDto>>(billsFromHouseGroup);

            }

            var bills = await _bills
                .Where(bill => bill.IsPaid == request.IsPaid &&
                    bill.DateOfPayment.Month == request.Month &&
                    bill.DateOfPayment.Year == request.Year)
                .Select(bill => _mapper.Map<BillDto>(bill))
                .ToListAsync();

            return new Response<List<BillDto>>(bills);
        }
    }
}
