using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.ValueObjects;

namespace SharedHome.Tests.Shared.Providers
{
    public static class HouseGroupProvider
    {
        public const string PersonId = "99c3dce2-54ca-48d6-a8cd-faca83168768";
        public const int HouseGroupId = 0;

        public static HouseGroup Get()
            => HouseGroup.Create();

        public static HouseGroup GetWithMember(bool isOwner = true)
        {
            var houseGroup =  HouseGroup.Create();
            houseGroup.AddMember(new HouseGroupMember(HouseGroupId, PersonId, isOwner));

            return houseGroup;
        }

        public static HouseGroup GetWithAdditionalMembers(int additionalMembersCount = 1)
        {
            var houseGroup = GetWithMember();
            for (var i = 0; i < additionalMembersCount; i++)
            {
                houseGroup.AddMember(new HouseGroupMember(HouseGroupId, $"{PersonId}{i}"));
            }

            return houseGroup;
        }

        public static HouseGroup GetWithMembersLimit()
        {
            var houseGroup = GetWithAdditionalMembers(4);

            return houseGroup;
        }
    }
}
