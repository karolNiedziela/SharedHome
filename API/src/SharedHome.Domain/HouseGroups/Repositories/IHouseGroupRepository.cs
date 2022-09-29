using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.ShoppingLists.Aggregates;

namespace SharedHome.Domain.HouseGroups.Repositories
{
    public interface IHouseGroupRepository
    {
        Task<HouseGroup?> GetAsync(string personId);

        Task<HouseGroup?> GetAsync(int houseGroupId, string personId);

        Task AddAsync(HouseGroup houseGroup);

        Task UpdateAsync(HouseGroup houseGroup);

        Task DeleteAsync(HouseGroup houseGroup);

    }
}
