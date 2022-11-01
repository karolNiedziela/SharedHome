using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Application.HouseGroups.Extensions
{
    public static class HouseGroupRepositoryExtensions
    {
        public static async Task<HouseGroup> GetOrThrowAsync(this IHouseGroupRepository houseGroupRepository,
            HouseGroupId houseGroupId, PersonId personId)
        {
            var houseGroup = await houseGroupRepository.GetAsync(houseGroupId, personId);
            if (houseGroup is null)
            {
                throw new HouseGroupNotFoundException(houseGroupId);
            }

            return houseGroup;
        }
    }
}
