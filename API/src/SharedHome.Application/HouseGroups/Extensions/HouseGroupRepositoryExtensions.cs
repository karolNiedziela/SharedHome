using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;

namespace SharedHome.Application.HouseGroups.Extensions
{
    public static class HouseGroupRepositoryExtensions
    {
        public static async Task<HouseGroup> GetOrThrowAsync(this IHouseGroupRepository houseGroupRepository,
            int houseGroupId, string personId)
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
