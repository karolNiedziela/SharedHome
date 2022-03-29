using SharedHome.Domain.HouseGroups.Events;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.HouseGroups.Aggregates
{
    public class HouseGroup : Entity, IAggregateRoot
    {        
        public const int MaxMembers = 5;

        private readonly List<HouseGroupMember> _members = new();


        public int Id { get; set; }

        public IEnumerable<HouseGroupMember> Members => _members;

        public int TotalMembers => _members.Count;

        private HouseGroup()
        {

        }

        public static HouseGroup Create(HouseGroupMember houseGroupMember)
        {
            var houseGroup = new HouseGroup();
            houseGroup.AddMember(houseGroupMember);

            return houseGroup;
        }

        public void AddMember(HouseGroupMember houseGroupMember)
        {
            ValidateMembersLimit();

            if (_members.Any(hgm => hgm.PersonId == houseGroupMember.PersonId))
            {
                throw new PersonIsAlreadyInHouseGroupException(houseGroupMember.PersonId);
            }

            _members.Add(houseGroupMember);

            AddEvent(new HouseGroupMemberAdded(Id, houseGroupMember));
        }

        public void RemoveMember(string requestedById, string memberToRemoveId)
        {
            var requester = GetMember(requestedById);
            if (!requester.IsOwner)
            {
                throw new HouseGroupMemberIsNotOwnerException(requestedById);
            }

            var memberToRemove = GetMember(memberToRemoveId);

            _members.Remove(memberToRemove);

            AddEvent(new HouseGroupMemberRemoved(Id, memberToRemoveId));
        }

        // Change owner of house group, only current owner can give this status to another member
        public void HandOwnerRoleOver(string oldOwnerPersonId, string newOwnerPersonid)
        {
            var memberOldOwner = GetMember(oldOwnerPersonId);
            if (!memberOldOwner.IsOwner)
            {
                throw new HouseGroupMemberIsNotOwnerException(oldOwnerPersonId);
            }

            var memberNewOwner = GetMember(newOwnerPersonid);

            var oldOwner = memberOldOwner with { IsOwner = false };
            var newOwner = memberNewOwner with { IsOwner = true };

            var oldOwnerIndex = _members.FindIndex(hm => hm.PersonId == oldOwnerPersonId);
            _members[oldOwnerIndex] = oldOwner;

            var newOwnerIndex = _members.FindIndex(hm => hm.PersonId == newOwnerPersonid);
            _members[newOwnerIndex] = newOwner;

            AddEvent(new HouseGroupOwnerChanged(Id, oldOwner, newOwner));
        }

        private HouseGroupMember GetMember(string personId)
        {
            var member = _members.SingleOrDefault(x => x.PersonId == personId);
            if (member is null)
            {
                throw new HouseGroupMemberNotFoundException(personId);
            }

            return member;
        }

        private void ValidateMembersLimit()
        {
            if (TotalMembers >= MaxMembers)
            {
                throw new TotalMembersLimitReachedException(MaxMembers);
            }
        }
    }
}
