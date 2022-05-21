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

        public int Id { get; private set; }

        public IEnumerable<HouseGroupMember> Members => _members;

        public int TotalMembers => _members.Count;

        private HouseGroup()
        {

        }

        public static HouseGroup Create()
        {
            var houseGroup = new HouseGroup();

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

            memberOldOwner.UnmarkAsOwner();
            memberNewOwner.MarkAsOwner();
            
            var oldOwnerIndex = _members.FindIndex(hm => hm.PersonId == oldOwnerPersonId);
            _members[oldOwnerIndex] = memberOldOwner;

            var newOwnerIndex = _members.FindIndex(hm => hm.PersonId == newOwnerPersonid);
            _members[newOwnerIndex] = memberNewOwner;

            AddEvent(new HouseGroupOwnerChanged(Id, memberOldOwner, memberNewOwner));
        }

        public void Leave(string personId, string? newOwnerPersonId = null)
        {
            ValidateOnLeaving(personId, newOwnerPersonId);

            var member = GetMember(personId);
            if (ShouldProcessForOwner(personId))
            {
                HandOwnerRoleOver(personId, newOwnerPersonId!);
                _members.Remove(member);
                return;
            }

            _members.Remove(member);
        }

        public bool IsOwner(string personId)
        {
            var member = GetMember(personId);

            return member.IsOwner;
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

        private void ValidateOnLeaving(string personId, string? newOwnerPersonId = null)
        {
            if (ShouldProcessForOwner(personId) && string.IsNullOrEmpty(newOwnerPersonId))
            {
                throw new LeavingHouseGroupNewOwnerNotDefinedException();
            }
        }
        
        private bool ShouldProcessForOwner(string personId)
            => IsOwner(personId) && _members.Count() > 1;
    }
}
