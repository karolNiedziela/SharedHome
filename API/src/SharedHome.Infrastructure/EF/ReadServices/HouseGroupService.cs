using Microsoft.EntityFrameworkCore;
using SharedHome.Application.ReadServices;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.ReadServices
{
    internal class HouseGroupService : IHouseGroupReadService
    {
        private readonly DbSet<HouseGroupReadModel> _houseGroups;

        public HouseGroupService(ReadSharedHomeDbContext context)
        {
            _houseGroups = context.HouseGroups;
        }

        public async Task<bool> IsPersonInHouseGroup(Guid personId)
          => await _houseGroups
            .Include(houseGroup => houseGroup.Members)
          .AnyAsync(houseGroup => houseGroup.Members
              .Any(member => member.PersonId == personId));

        public async Task<bool> IsPersonInHouseGroup(Guid personId, Guid houseGroupId)
            => await _houseGroups
            .AnyAsync(houseGroup => houseGroup.Id == houseGroupId && houseGroup.Members
                .Any(member => member.PersonId == personId));      
        
        public async Task<IEnumerable<Guid>> GetMemberPersonIds(Guid personId)
             => await _houseGroups
               .Include(houseGroup => houseGroup.Members
                 .Where(member => member.PersonId == personId))
               .SelectMany(houseGroup => houseGroup.Members
               .Select(member => member.PersonId))
               .ToListAsync();

        public async Task<IEnumerable<Guid>> GetMemberPersonIdsExcludingCreator(Guid personId) 
            => await _houseGroups
               .Include(houseGroup => houseGroup.Members)
               .SelectMany(houseGroup => houseGroup.Members
                 .Where(member => member.PersonId != personId)
               .Select(member => member.PersonId))
               .ToListAsync();
    }
}
