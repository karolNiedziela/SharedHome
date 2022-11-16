using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.HouseGroups.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using MediatR;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Infrastructure.EF.Queries.HouseGroups.Handlers
{
    internal class GetHouseGroupHandler : IRequestHandler<GetHouseGroup, Response<HouseGroupDto>>
    {
        private readonly DbSet<HouseGroupReadModel> _houseGroups;
        private readonly IMapper _mapper;

        public GetHouseGroupHandler(ReadSharedHomeDbContext context, IMapper mapper)
        {
            _houseGroups = context.HouseGroups;
            _mapper = mapper;
        }

        public async Task<Response<HouseGroupDto>> Handle(GetHouseGroup request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroups
                .Include(hg => hg.Members
                    .OrderByDescending(member => member.IsOwner))
                .ThenInclude(m => m.Person)
                .Where(houseGroup => houseGroup.Members.Any(member => member.PersonId == request.PersonId))
                .FirstOrDefaultAsync();

            return new Response<HouseGroupDto>(_mapper.Map<HouseGroupDto>(houseGroup!));
        }
    }
}
