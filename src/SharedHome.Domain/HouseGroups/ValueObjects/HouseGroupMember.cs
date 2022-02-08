using SharedHome.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.ValueObjects
{
    public record HouseGroupMember
    {
        public Guid PersonId { get; }

        public FullName FullName { get; }

        public Email Email { get; }

        public bool IsOwner { get; init; }

        public HouseGroupMember(Guid personId, FullName fullName, Email email, bool isOwner = false)
        {
            PersonId = personId;
            FullName = fullName;
            Email = email;
            IsOwner = isOwner;
        }
    }
}
