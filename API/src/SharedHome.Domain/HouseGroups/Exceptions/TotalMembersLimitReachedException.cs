using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class TotalMembersLimitReachedException : SharedHomeException
    {
        public override string ErrorCode => "TotalMembersLimitReached";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        [Order]
        public int TotalMembers { get; }

        public TotalMembersLimitReachedException(int totalMembers) 
            : base($"Max members limit reached. Limit is {totalMembers}.")
        {
            TotalMembers = totalMembers;
        }

    }
}
