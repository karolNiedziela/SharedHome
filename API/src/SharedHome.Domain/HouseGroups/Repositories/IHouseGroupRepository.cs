using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Repositories
{
    public interface IHouseGroupRepository
    {
        Task<HouseGroup> GetAsync(int houseGroupId);

        Task AddAsync(HouseGroup shoppingList);

        Task UpdateAsync(HouseGroup shoppingList);

        Task DeleteAsync(HouseGroup shoppingList);
    }
}
