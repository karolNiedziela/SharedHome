using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.ValueObjects;

namespace SharedHome.Application.UnitTests.Providers
{
    public static class HouseGroupProvider
    {
        public const string DefaultPersonId = "personId";
        public const string DefaultPersonFirstName = "FirstName";
        public const string DefaultPersonLastName = "LastName";
        public const string DefaultPersonEmail = "person@email.com";

        public static HouseGroup GetWithOwner()
        {
            var houseGroup =  HouseGroup.Create(new HouseGroupMember(DefaultPersonId, DefaultPersonFirstName,
                DefaultPersonLastName, DefaultPersonEmail, true));
            houseGroup.Id = 1;

            return houseGroup;
        }

        public static HouseGroup GetWithAdditionalMembers(int additionalMembersCount = 1)
        {
            var houseGroup = GetWithOwner();
            for (var i = 0; i < additionalMembersCount; i++)
            {
                houseGroup.AddMember(new HouseGroupMember($"{DefaultPersonId}{i}",
                    DefaultPersonFirstName, DefaultPersonLastName, DefaultPersonEmail));
            }

            return houseGroup;
        }
    }
}
