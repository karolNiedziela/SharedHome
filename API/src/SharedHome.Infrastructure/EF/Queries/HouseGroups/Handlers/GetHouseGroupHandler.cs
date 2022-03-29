using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.DTO;
using SharedHome.Application.HouseGroups.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.HouseGroups.Handlers
{
    public class GetHouseGroupHandler : IQueryHandler<GetHouseGroup, Response<HouseGroupDto>>
    {
        private readonly SharedHomeDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetHouseGroupHandler(SharedHomeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<HouseGroupDto>> Handle(GetHouseGroup request, CancellationToken cancellationToken)
        {
            var houseGroup = await _dbContext.HouseGroups
                .Include(hg => hg.Members
                    .OrderByDescending(member => member.IsOwner))
                .Where(houseGroup => houseGroup.Members.Any(member => member.PersonId == request.PersonId))
                .FirstOrDefaultAsync();

            return new Response<HouseGroupDto>(_mapper.Map<HouseGroupDto>(houseGroup));
        }
    }
}
