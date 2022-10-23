using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.HouseGroups.Repositories
{
    public interface IHouseGroupRepository
    {
        Task<HouseGroup?> GetAsync(PersonId personId);

        Task<HouseGroup?> GetAsync(HouseGroupId houseGroupId, PersonId personId);

        Task AddAsync(HouseGroup houseGroup);

        Task UpdateAsync(HouseGroup houseGroup);

        Task DeleteAsync(HouseGroup houseGroup);

    }
}
