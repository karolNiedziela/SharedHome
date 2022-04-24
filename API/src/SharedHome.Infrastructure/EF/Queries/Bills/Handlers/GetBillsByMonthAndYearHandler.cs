using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Abstractions.Time;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    public class GetBillsByMonthAndYearHandler : IQueryHandler<GetBillsByMonthAndYear, Response<List<BillDto>>>
    {
        private readonly SharedHomeDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITime _time;

        public GetBillsByMonthAndYearHandler(SharedHomeDbContext context, IMapper mapper, ITime time)
        {
            _context = context;
            _mapper = mapper;
            _time = time;
        }

        public async Task<Response<List<BillDto>>> Handle(GetBillsByMonthAndYear request, CancellationToken cancellationToken)
        {
            if (!request.Year.HasValue || !request.Month.HasValue)
            {
                var currentDate = _time.CurrentDate();
                request.Year = currentDate.Year;
                request.Month = currentDate.Month;
            }

            var bills = await _context.Bills
                .Where(bill => bill.CreatedAt.Month == request.Month &&
                bill.CreatedAt.Year == request.Year)
                .ToListAsync();

            return new Response<List<BillDto>>(_mapper.Map<List<BillDto>>(bills));
        }
    }
}
