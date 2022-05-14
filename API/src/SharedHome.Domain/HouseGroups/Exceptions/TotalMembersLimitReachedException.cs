using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class TotalMembersLimitReachedException : SharedHomeException
    {
        public int TotalMembers { get; }

        public TotalMembersLimitReachedException(int totalMembers) : base($"Max members limit reached. Limit is {totalMembers}.")
        {
            TotalMembers = totalMembers;
        }

    }
}
