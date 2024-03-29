﻿using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.HouseGroups.Repositories
{
    public interface IHouseGroupRepository
    {
        Task<HouseGroup?> GetAsync(PersonId personId);

        Task<HouseGroup?> GetAsync(HouseGroupId houseGroupId, PersonId personId);

        Task<bool> IsPersonInHouseGroupAsync(PersonId personId);

        Task<bool> IsPersonInHouseGroupAsync(PersonId personId, HouseGroupId houseGroupId);

        Task<IEnumerable<Guid>> GetMemberPersonIdsAsync(PersonId personId);

        Task<IEnumerable<Guid>> GetMemberPersonIdsExcludingCreatorAsync(PersonId personId);

        Task AddAsync(HouseGroup houseGroup);

        Task UpdateAsync(HouseGroup houseGroup);

        Task DeleteAsync(HouseGroup houseGroup);

    }
}
