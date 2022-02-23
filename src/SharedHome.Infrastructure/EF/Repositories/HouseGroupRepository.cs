using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Repositories
{
    internal sealed class HouseGroupRepository : IHouseGroupRepository
    {
        public Task AddAsync(HouseGroup shoppingList)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(HouseGroup shoppingList)
        {
            throw new NotImplementedException();
        }

        public Task<HouseGroup> GetAsync(int houseGroupId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(HouseGroup shoppingList)
        {
            throw new NotImplementedException();
        }
    }
}
