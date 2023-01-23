using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Entities;

namespace SharedHome.Tests.Shared.Providers
{
    public static class HouseGroupFakeProvider
    {
        public static readonly Guid PersonId = new("99c3dce2-54ca-48d6-a8cd-faca83168768");
        public static readonly Guid HouseGroupId = new("56b47fac-bd9f-47b7-8ab3-13139f5cfd95");
        public const string DefaultHouseGroupName = "HouseGroup";

        public static HouseGroup Get()
            => HouseGroup.Create(HouseGroupId, DefaultHouseGroupName);

        public static HouseGroup GetWithMember(bool isOwner = true)
        {
            var houseGroup =  HouseGroup.Create(HouseGroupId, DefaultHouseGroupName);
            houseGroup.AddMember(new HouseGroupMember(HouseGroupId, PersonId, isOwner));

            return houseGroup;
        }

        public static HouseGroup GetWithAdditionalMembers(IEnumerable<Guid> memberIds)
        {
            var houseGroup = GetWithMember();
            foreach (var memberId in memberIds)
            {
                houseGroup.AddMember(new HouseGroupMember(HouseGroupId, memberId));
            }

            return houseGroup;
        }

        public static HouseGroup GetWithMembersLimit()
        {
            var houseGroup = GetWithAdditionalMembers(new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() });

            return houseGroup;
        }
    }
}
