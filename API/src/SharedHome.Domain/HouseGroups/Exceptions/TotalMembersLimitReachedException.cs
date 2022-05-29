using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class TotalMembersLimitReachedException : SharedHomeException
    {
        public override string ErrorCode => "TotalMembersLimitReached";

        [Order]
        public int TotalMembers { get; }

        public TotalMembersLimitReachedException(int totalMembers) : base($"Max members limit reached. Limit is {totalMembers}.")
        {
            TotalMembers = totalMembers;
        }

    }
}
