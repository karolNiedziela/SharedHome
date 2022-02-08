using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupMemberWasNotFoundException : SharedHomeException
    {
        public Guid PersonId { get; }

        public HouseGroupMemberWasNotFoundException(Guid personId) : base($"House group member with person id '{personId}' was not found.")
        {
            PersonId = personId;
        }

    }
}
