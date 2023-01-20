using SharedHome.Domain.Invitations;

namespace SharedHome.Tests.Shared.Providers
{
    public static class InvitationProvider
    {
        public static readonly Guid InvitationId = new("5f140af2-6979-41ab-af1f-97dec47048ee");
        public static readonly Guid HouseGroupId = new("56b47fac-bd9f-47b7-8ab3-13139f5cfd95");
        public static readonly Guid RequestedByPersonId = new("782784d7-0fee-4d7d-a1ff-68689dd340ef");
        public static readonly Guid RequestedToPersonId = new("9cbcaf55-47b2-49b9-a682-14489c1912cf");

        public static Invitation Get()
            => Invitation.CreatePending(InvitationId, HouseGroupId, RequestedByPersonId, RequestedToPersonId);        
    }
}
