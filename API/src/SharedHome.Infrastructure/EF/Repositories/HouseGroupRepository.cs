using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Repositories
{
    internal sealed class HouseGroupRepository : IHouseGroupRepository
    {
        private readonly WriteSharedHomeDbContext _dbContext;

        public HouseGroupRepository(WriteSharedHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HouseGroup?> GetAsync(PersonId personId)
             => await _dbContext.HouseGroups
            .Include(houseGroup => houseGroup.Members.Where(m => m.PersonId == personId))
            .FirstOrDefaultAsync();

        public async Task<HouseGroup?> GetAsync(HouseGroupId houseGroupId, PersonId personId)
            => await _dbContext.HouseGroups
            .Include(houseGroup => houseGroup.Members)
            .FirstOrDefaultAsync(houseGroup => houseGroup.Id == houseGroupId);

        public async Task<bool> IsPersonInHouseGroupAsync(PersonId personId)
         => await _dbContext.HouseGroups
           .Include(houseGroup => houseGroup.Members)
         .AnyAsync(houseGroup => houseGroup.Members
             .Any(member => member.PersonId == personId));

        public async Task<bool> IsPersonInHouseGroupAsync(PersonId personId, HouseGroupId houseGroupId)
            => await _dbContext.HouseGroups
            .AnyAsync(houseGroup => houseGroup.Id == houseGroupId && houseGroup.Members
                .Any(member => member.PersonId == personId));

        public async Task<IEnumerable<Guid>> GetMemberPersonIdsAsync(PersonId personId)
             => await _dbContext.HouseGroups
               .Include(houseGroup => houseGroup.Members)
               .Where(houseGroup => houseGroup.Members
                .Any(member => member.PersonId == personId))
               .SelectMany(houseGroup => houseGroup.Members)
               .Select(member => member.PersonId.Value)
               .ToListAsync();

        public async Task<IEnumerable<Guid>> GetMemberPersonIdsExcludingCreatorAsync(PersonId personId)
            => await _dbContext.HouseGroups
               .Include(houseGroup => houseGroup.Members)
               .Where(houseGroup => houseGroup.Members
                .Any(member => member.PersonId == personId))
               .SelectMany(houseGroup => houseGroup.Members
                 .Where(member => member.PersonId != personId)
               .Select(member => member.PersonId.Value))
               .ToListAsync();

        public async Task AddAsync(HouseGroup houseGroup)
        {
            await _dbContext.HouseGroups.AddAsync(houseGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(HouseGroup houseGroup)
        {
            _dbContext.HouseGroups.Remove(houseGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(HouseGroup houseGroup)
        {
            _dbContext.HouseGroups.Update(houseGroup);
            await _dbContext.SaveChangesAsync();
        }       
    }
}
