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
        public const string DefaultPersonId = "personId";
        public const string DefaultFirstName = "FirstName";
        public const string DefaultLastName = "LastName";

        public static Invitation Get()
            => Invitation.Create(DefaultHouseGroupId, DefaultPersonId, DefaultFirstName, DefaultLastName);        
    }
}
