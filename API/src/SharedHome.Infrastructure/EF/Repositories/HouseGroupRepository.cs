﻿using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Repositories
{
    internal sealed class HouseGroupRepository : IHouseGroupRepository
    {
        private readonly SharedHomeDbContext _dbContext;

        public HouseGroupRepository(SharedHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HouseGroup?> GetAsync(int houseGroupId, string personId)
            => await _dbContext.HouseGroups
            .Include(houseGroup => houseGroup.Members
                .Where(member => member.PersonId == personId))
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

        public async Task<bool> IsPersonInHouseGroup(string personId)
            => await _dbContext.HouseGroups
            .AnyAsync(houseGroup => houseGroup.Members
                .Any(member => member.PersonId == personId));

        public async Task<bool> IsPersonInHouseGroup(string personId, int houseGroupId)
            => await _dbContext.HouseGroups
            .AnyAsync(houseGroup => houseGroup.Id == houseGroupId && houseGroup.Members
                .Any(member => member.PersonId == personId));
    }
}
