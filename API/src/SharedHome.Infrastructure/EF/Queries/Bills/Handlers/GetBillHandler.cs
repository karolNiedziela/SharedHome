using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    public class GetBillHandler : IQueryHandler<GetBill, Response<BillDto>>
    {
        private readonly SharedHomeDbContext _context;
        private readonly IMapper _mapper;

        public GetBillHandler(SharedHomeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<BillDto>> Handle(GetBill request, CancellationToken cancellationToken)
        {
            var bill = await _context.Bills
                .SingleOrDefaultAsync(bill => bill.PersonId == request.PersonId && bill.Id == request.Id);

            return new Response<BillDto>(_mapper.Map<BillDto>(bill));
        }
    }
}
