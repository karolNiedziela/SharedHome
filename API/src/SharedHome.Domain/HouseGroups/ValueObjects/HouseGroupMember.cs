using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.HouseGroups.ValueObjects
{
    public class HouseGroupMember : Entity
    {
        public string PersonId { get; private set; } = default!;

        public int HouseGroupId { get; private set; }

        public bool IsOwner { get; private set; }

        private HouseGroupMember()
        {

        }

        public HouseGroupMember(int houseGroupId, string personId, bool isOwner = false)
        {
            HouseGroupId = houseGroupId;
            PersonId = personId;
            IsOwner = isOwner;
        }

        public void MarkAsOwner() => IsOwner = true;

        public void UnmarkAsOwner() => IsOwner = false;
    }
}
