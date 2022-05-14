using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.ValueObjects;
using System;

namespace SharedHome.Application.UnitTests.Providers
{
    public static class HouseGroupProvider
    {
        public const string DefaultPersonId = "99c3dce2-54ca-48d6-a8cd-faca83168768";
        public const int DefaultHouseGroupId = 0;

        public static HouseGroup GetWithOwner()
        {
            var houseGroup =  HouseGroup.Create();
            houseGroup.AddMember(new HouseGroupMember(DefaultHouseGroupId, DefaultPersonId, true));

            return houseGroup;
        }

        public static HouseGroup GetWithAdditionalMembers(int additionalMembersCount = 1)
        {
            var houseGroup = GetWithOwner();
            for (var i = 0; i < additionalMembersCount; i++)
            {
                houseGroup.AddMember(new HouseGroupMember(DefaultHouseGroupId, $"{DefaultPersonId}{i}"));
            }

            return houseGroup;
        }
    }
}
