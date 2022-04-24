using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Services;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Services
{
    public class HouseGroupService : IHouseGroupService
    {
        private readonly SharedHomeDbContext _dbContext;

        public HouseGroupService(SharedHomeDbContext dbContext)
        {
            _dbContext = dbContext;
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
