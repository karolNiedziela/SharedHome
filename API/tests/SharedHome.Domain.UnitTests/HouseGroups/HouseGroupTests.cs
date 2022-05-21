using SharedHome.Domain.HouseGroups.Events;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System.Linq;
using Xunit;

namespace SharedHome.Domain.UnitTests.HouseGroups
{
    public class HouseGroupTests
    {
        [Fact]
        public void AddMember_Throws_TotalMembersLimitReachedException_When_Members_Limit_Reached()
        {
            var houseGroup = HouseGroupProvider.GetWithMembersLimit();

            var exception = Record.Exception(() 
                => houseGroup.AddMember(new HouseGroupMember
                (
                    HouseGroupProvider.HouseGroupId, 
                HouseGroupProvider.PersonId)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<TotalMembersLimitReachedException>();
        }

        [Fact]
        public void AddMember_Throws_PersonIsAlreadyInHouseGroupException_When_Member_Is_Already_In_Group()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            var exception = Record.Exception(() 
                => houseGroup.AddMember(
                    new HouseGroupMember(
                        HouseGroupProvider.HouseGroupId, 
                        HouseGroupProvider.PersonId, 
                        false)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PersonIsAlreadyInHouseGroupException>();
        }

        [Fact]
        public void AddMember_Adds_HouseGroupMemberAdded_OnSuccess()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            houseGroup.Members.Count().ShouldBe(1);
            houseGroup.Events.Count().ShouldBe(1);

            var @event = houseGroup.Events.FirstOrDefault() as HouseGroupMemberAdded;
            @event.ShouldNotBeNull();
            @event.HouseGroupMember.HouseGroupId.ShouldBe(houseGroup.Id);
        }

        [Fact]
        public void RemoveMember_Throws_HouseGroupMemberWasNotFoundException_When_Requested_By_Not_Found()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            var exception = Record.Exception(() => houseGroup.RemoveMember("", ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberNotFoundException>();
        }

        [Fact]
        public void RemoveMember_Throws_HouseGroupMemberIsNotOwnerBeforeChangeException_When_Requested_By_Is_Not_Owner()
        {
            var houseGroup = HouseGroupProvider.GetWithMember(false);

            var exception = Record.Exception(() 
                => houseGroup.RemoveMember(HouseGroupProvider.PersonId, ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberIsNotOwnerException>();
        }

        [Fact]
        public void RemoveMember_Throws_HouseGroupMemberWasNotFoundException_When_Member_To_Remove_Was_Not_Found()
        {
            var houseGroup = HouseGroupProvider.Get();

            var exception = Record.Exception(() 
                => houseGroup.RemoveMember(HouseGroupProvider.PersonId, ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberNotFoundException>();
        }

        [Fact]
        public void RemoveMember_Adds_HouseGroupMemberRemoved_On_Success()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            var memberToRemoveId = "";
            houseGroup.AddMember(new HouseGroupMember
                (
                HouseGroupProvider.HouseGroupId, 
                memberToRemoveId, 
                false));
            houseGroup.ClearEvents();

            houseGroup.RemoveMember(HouseGroupProvider.PersonId, memberToRemoveId);

            houseGroup.Members.Count().ShouldBe(1);
            houseGroup.Events.Count().ShouldBe(1);

            var @event = houseGroup.Events.FirstOrDefault() as HouseGroupMemberRemoved;
            @event.ShouldNotBeNull();
            @event.RemovedMemberId.ShouldBe(memberToRemoveId);
        }

        [Fact]
        public void HandOwnerRoleOver_Throws_HouseGroupMemberIsNotOwnerException_When_Person_Changing_Owner_Is_Not_Owner()
        {
            var houseGroup = HouseGroupProvider.GetWithMember(false);

            var exception = Record.Exception(() 
                => houseGroup.HandOwnerRoleOver(HouseGroupProvider.PersonId, ""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupMemberIsNotOwnerException>();
        }

        [Fact]
        public void HandOwnerRoleOver_Adds_HouseGroupOwnerChanged_On_Success()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            var secondPersonId = "";
            houseGroup.AddMember(new HouseGroupMember
                (
                HouseGroupProvider.HouseGroupId, 
                secondPersonId, 
                false));
            houseGroup.ClearEvents();

            houseGroup.HandOwnerRoleOver(HouseGroupProvider.PersonId, secondPersonId);

            houseGroup.Events.Count().ShouldBe(1);
            var @event = houseGroup.Events.FirstOrDefault() as HouseGroupOwnerChanged;
            @event.ShouldNotBeNull();
            @event.OldOwner.IsOwner.ShouldBeFalse();
            @event.NewOwner.IsOwner.ShouldBeTrue();
        }

        [Fact]
        public void Leave_Should_Throw_LeavingHouseGroupNewOwnerNotDefinedException_When_Leaving_Member_Is_Owner_And_Members_Count_Is_Greater_Than_Zero()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();
            houseGroup.AddMember(new HouseGroupMember(0, "memberId"));

            var exception = Record.Exception(() => houseGroup.Leave(HouseGroupProvider.PersonId));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<LeavingHouseGroupNewOwnerNotDefinedException>();
        }

        [Fact]
        public void Leave_Should_Not_Throw_LeavingHouseGroupNewOwnerNotDefinedException_When_Leaving_Member_Is_Owner_And_Members_Count_Is_Greater_Than_Zero()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            var exception = Record.Exception(() => houseGroup.Leave(HouseGroupProvider.PersonId));

            exception.ShouldBeNull();
        }

        [Fact]
        public void Leave_Should_Remove_Member_And_Set_New_Owner_When_Leaving_Member_Is_Owner()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();
            houseGroup.AddMember(new HouseGroupMember(0, "secondMemberId"));

            houseGroup.Leave(HouseGroupProvider.PersonId, "secondMemberId");

            houseGroup.Members.Count().ShouldBe(1);
            houseGroup.Members.First().IsOwner.ShouldBeTrue();
        }

        [Fact]
        public void Leave_Should_Remove_Member_When_Member_Is_Not_Owner()
        {
            var houseGroup = HouseGroupProvider.GetWithMember(false);

            houseGroup.Leave(HouseGroupProvider.PersonId);

            houseGroup.Members.Count().ShouldBe(0);
        }
    }
}
