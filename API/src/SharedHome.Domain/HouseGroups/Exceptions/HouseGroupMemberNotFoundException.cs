using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupMemberNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "HouseGroupMemberNotFoundException";

        public string PersonId { get; }

        public HouseGroupMemberNotFoundException(string personId) : base($"House group member with person id: '{personId}' was not found.")
        {
            PersonId = personId;
        }

    }
}
