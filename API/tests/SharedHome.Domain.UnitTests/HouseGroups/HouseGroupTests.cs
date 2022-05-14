using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Events;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.ValueObjects;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace SharedHome.Domain.UnitTests.HouseGroups
{
    public class HouseGroupTests
    {
        private readonly int _houseGroupId = 1;
        private readonly string _personId = "46826ecb-c40d-441c-ad0d-f11e616e4948";

        [Fact]
        public void AddMember_Throws_TotalMembersLimitReachedException_When_Members_Limit_Reached()
        {
            var houseGroup = GetHouseGroupWithMembersLimit();

            var exception = Record.Exception(() => houseGroup.AddMember(new HouseGroupMember(_houseGroupId, _personId)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<TotalMembersLimitReachedException>();
        }

        [Fact]
        public void AddMember_Throws_PersonIsAlreadyInHouseGroupException_When_Member_Is_Already_In_Group()
        {
            var houseGroup = GetHouseGroupWithMember();

            var exception = Record.Exception(() => houseGroup.AddMember(new HouseGroupMember(_houseGroupId, _personId, false)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PersonIsAlreadyInHouseGroupException>();
        }

        [Fact]
        public void AddMember_Adds_HouseGroupMemberAdded_OnSuccess()
        {
            var houseGroup = GetHouseGroupWithMember();

            houseGroup.Members.Count().ShouldBe(1);
            houseGroup.Events.Count().ShouldBe(1);

            var @event = houseGroup.Events.FirstOrDefault() as HouseGroupMemberAdded;
            @event.ShouldNotBeNull();
            @event.HouseGroupMember.HouseGroupId.ShouldBe(houseGroup.Id);
        }

        [Fact]
        public void RemoveMember_Throws_HouseGroupMemberWasNotFoundException_When_Requested_By_Not_Found()
        {
            var houseGroup = GetHouseGroupWithMember();

            var exception = Record.Exception(() => houseGroup.RemoveMember("", ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberNotFoundException>();
        }

        [Fact]
        public void RemoveMember_Throws_HouseGroupMemberIsNotOwnerBeforeChangeException_When_Requested_By_Is_Not_Owner()
        {
            var houseGroup = GetHouseGroupWithMember(false);

            var exception = Record.Exception(() => houseGroup.RemoveMember(_personId, ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberIsNotOwnerException>();
        }

        [Fact]
        public void RemoveMember_Throws_HouseGroupMemberWasNotFoundException_When_Member_To_Remove_Was_Not_Found()
        {
            var houseGroup = GetHouseGroup();

            var exception = Record.Exception(() => houseGroup.RemoveMember(_personId, ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberNotFoundException>();
        }

        [Fact]
        public void RemoveMember_Adds_HouseGroupMemberRemoved_On_Success()
        {
            var houseGroup = GetHouseGroupWithMember();

            var memberToRemoveId = "";
            houseGroup.AddMember(new HouseGroupMember(_houseGroupId, memberToRemoveId, false));
            houseGroup.ClearEvents();

            houseGroup.RemoveMember(_personId, memberToRemoveId);

            houseGroup.Members.Count().ShouldBe(1);
            houseGroup.Events.Count().ShouldBe(1);

            var @event = houseGroup.Events.FirstOrDefault() as HouseGroupMemberRemoved;
            @event.ShouldNotBeNull();
            @event.RemovedMemberId.ShouldBe(memberToRemoveId);
        }

        [Fact]
        public void HandOwnerRoleOver_Throws_HouseGroupMemberIsNotOwnerException_When_Person_Changing_Owner_Is_Not_Owner()
        {
            var houseGroup = GetHouseGroupWithMember(false);

            var exception = Record.Exception(() => houseGroup.HandOwnerRoleOver(_personId, ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberIsNotOwnerException>();
        }

        [Fact]
        public void HandOwnerRoleOver_Adds_HouseGroupOwnerChanged_On_Success()
        {
            var houseGroup = GetHouseGroupWithMember();

            var secondPersonId = "";
            houseGroup.AddMember(new HouseGroupMember(_houseGroupId, secondPersonId, false));
            houseGroup.ClearEvents();

            houseGroup.HandOwnerRoleOver(_personId, secondPersonId);

            houseGroup.Events.Count().ShouldBe(1);
            var @event = houseGroup.Events.FirstOrDefault() as HouseGroupOwnerChanged;
            @event.ShouldNotBeNull();
            @event.OldOwner.IsOwner.ShouldBeFalse();
            @event.NewOwner.IsOwner.ShouldBeTrue();
        }

        private HouseGroup GetHouseGroup() 
            => HouseGroup.Create();

        private HouseGroup GetHouseGroupWithMember(bool isOwner = true)
        {
            var houseGroup = GetHouseGroup();
            houseGroup.AddMember(new HouseGroupMember(0, _personId, isOwner));

            return houseGroup;
        }

        private HouseGroup GetHouseGroupWithMembersLimit()
        {
            var houseGroup = GetHouseGroup();
            for (var i = 0; i < HouseGroup.MaxMembers; i++)
            {
                houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, $"personId{i}"));
            }

            return houseGroup;
        }
    }
}
