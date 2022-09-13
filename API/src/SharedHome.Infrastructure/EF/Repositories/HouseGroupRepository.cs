using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
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

        public async Task<HouseGroup?> GetAsync(int houseGroupId, string personId)
            => await _dbContext.HouseGroups
            .Include(houseGroup => houseGroup.Members)
            .SingleOrDefaultAsync(houseGroup => houseGroup.Id == houseGroupId);

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
