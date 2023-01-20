using SharedHome.Domain.Primivites;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.HouseGroups.Entities
{
    public class HouseGroupMember : Entity
    {
        public PersonId PersonId { get; private set; } = default!;

        public HouseGroupId HouseGroupId { get; private set; } = default!;

        public bool IsOwner { get; private set; }

        private HouseGroupMember()
        {

        }

        public HouseGroupMember(HouseGroupId houseGroupId, PersonId personId, bool isOwner = false)
        {
            HouseGroupId = houseGroupId;
            PersonId = personId;
            IsOwner = isOwner;
        }

        public void MarkAsOwner() => IsOwner = true;

        public void UnmarkAsOwner() => IsOwner = false;
    }
}
