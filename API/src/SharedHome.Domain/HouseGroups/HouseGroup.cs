using SharedHome.Domain.Common.Models;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.HouseGroups
{
    public class HouseGroup : AggregateRoot<HouseGroupId>
    {        
        public const int MaxMembers = 5;

        private readonly List<HouseGroupMember> _members = new();

        public HouseGroupName Name { get; private set; } = default!;

        public IEnumerable<HouseGroupMember> Members => _members;      

        private HouseGroup()
        {

        }

        private HouseGroup(HouseGroupId id, HouseGroupName name)
        {
            Id = id;
            Name = name;
        }

        public static HouseGroup Create(HouseGroupId id,  HouseGroupName name)
            => new(id, name);

        public void AddMember(HouseGroupMember houseGroupMember)
        {
            ValidateMembersLimit();

            if (_members.Any(hgm => hgm.PersonId == houseGroupMember.PersonId))
            {
                throw new PersonIsAlreadyInHouseGroupException(houseGroupMember.PersonId);
            }

            _members.Add(houseGroupMember);
        }

        public void RemoveMember(PersonId requestedById, PersonId memberToRemoveId)
        {
            var requester = GetMember(requestedById);
            if (!requester.IsOwner)
            {
                throw new HouseGroupMemberIsNotOwnerException(requestedById);
            }

            var memberToRemove = GetMember(memberToRemoveId);

            _members.Remove(memberToRemove);
        }

        // Change owner of house group, only current owner can give this status to another member
        public void HandOwnerRoleOver(PersonId oldOwnerPersonId, PersonId newOwnerPersonid)
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
        }

        public void Leave(PersonId personId)
        {
            var member = GetMember(personId);

            if (IsOwner(personId) && _members.Count > 1)
            {
                var newOwnerPersonId = _members.First(x => !x.IsOwner).PersonId;
                HandOwnerRoleOver(personId, newOwnerPersonId);
                _members.Remove(member);
                return;
            }

            _members.Remove(member);
        }

        public bool IsOwner(PersonId personId)
        {
            var member = GetMember(personId);

            return member.IsOwner;
        }

        private HouseGroupMember GetMember(PersonId personId)
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
            if (_members.Count >= MaxMembers)
            {
                throw new TotalMembersLimitReachedException(MaxMembers);
            }
        }
    }
}
