using SharedHome.Domain.HouseGroups.Aggregates;

namespace SharedHome.Domain.HouseGroups.Repositories
{
    public interface IHouseGroupRepository
    {
        Task<HouseGroup?> GetAsync(int houseGroupId, string personId);

        Task AddAsync(HouseGroup houseGroup);

        Task UpdateAsync(HouseGroup houseGroup);

        Task DeleteAsync(HouseGroup houseGroup);

        Task<bool> IsPersonInHouseGroup(string personId);

        Task<bool> IsPersonInHouseGroup(string personId, int houseGroupId);
    }
}
