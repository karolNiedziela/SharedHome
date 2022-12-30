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
