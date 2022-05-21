using SharedHome.Domain.Invitations.Aggregates;

namespace SharedHome.Tests.Shared.Providers
{
    public static class InvitationProvider
    {
        public const int HouseGroupId = 1;
        public const string RequestedByPersonId = "782784d7-0fee-4d7d-a1ff-68689dd340ef";
        public const string RequestedToPersonId = "9cbcaf55-47b2-49b9-a682-14489c1912cf";
        public const string RequestedByFirstName = "FirstName";
        public const string RequestedByLastName = "LastName";

        public static Invitation Get()
            => Invitation.Create(HouseGroupId, RequestedByPersonId, RequestedToPersonId);        
    }
}
