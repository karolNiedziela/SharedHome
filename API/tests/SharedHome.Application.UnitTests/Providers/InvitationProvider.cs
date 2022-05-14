using SharedHome.Domain.Invitations.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.UnitTests.Providers
{
    public static class InvitationProvider
    {
        public const int DefaultHouseGroupId = 1;
        public const string DefaultRequestedByPersonId = "782784d7-0fee-4d7d-a1ff-68689dd340ef";
        public const string DefaultRequestedToPersonId = "9cbcaf55-47b2-49b9-a682-14489c1912cf";
        public const string DefaultRequestedByFirstName = "FirstName";
        public const string DefaultRequestedByLastName = "LastName";

        public static Invitation Get()
            => Invitation.Create(DefaultHouseGroupId, DefaultRequestedByPersonId, DefaultRequestedToPersonId);        
    }
}
