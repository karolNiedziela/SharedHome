using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Domain.UnitTests.Invitations
{
    public class InvitationTests
    {
        private int _houseGroupId = 1;
        private readonly string _requestedByPersonId = "46826ecb-c40d-441c-ad0d-f11e616e4948";
        private readonly string _requestedToPersonId = "9cbcaf55-47b2-49b9-a682-14489c1912cf";

        [Fact]
        public void NewInvitation_Should_Have_InvitationStatus_Set_To_Pending()
        {
            var invitation = GetInvitation();

            invitation.Status.ShouldBe(InvitationStatus.Pending);
        }

        [Fact]
        public void Accept_Throws_InvitationAcceptedException_When_Invitation_Is_Already_Accepted()
        {
            Invitation invitation = GetInvitation();
            invitation.Accept();

            var exception = Record.Exception(() => invitation.Accept());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationAcceptedException>();
        }

        [Fact]
        public void Accept_Throws_InvitationRejectedException_When_Invitation_Is_Already_Rejected()
        {
            Invitation invitation = GetInvitation();
            invitation.Reject();

            var exception = Record.Exception(() => invitation.Accept());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationRejectedException>();
        }

        [Fact]
        public void Accept_Should_Set_InvitationStatus_To_Accepted()
        {
            var invitation = GetInvitation();

            invitation.Accept();

            invitation.Status.ShouldBe(InvitationStatus.Accepted);
        }

        [Fact]
        public void Reject_Throws_InvitationAcceptedException_When_Invitation_Is_Already_Accepted()
        {
            Invitation invitation = GetInvitation();
            invitation.Accept();

            var exception = Record.Exception(() => invitation.Reject());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationAcceptedException>();
        }

        [Fact]
        public void Reject_Throws_InvitationRejectedException_When_Invitation_Is_Already_Rejected()
        {
            Invitation invitation = GetInvitation();
            invitation.Reject();

            var exception = Record.Exception(() => invitation.Reject());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationRejectedException>();
        }

        [Fact]
        public void Reject_Should_Set_InvitationStatus_To_Rejected()
        {
            var invitation = GetInvitation();

            invitation.Reject();

            invitation.Status.ShouldBe(InvitationStatus.Rejected);
        }


        private Invitation GetInvitation()
        {
            return Invitation.Create(_houseGroupId, _requestedByPersonId, _requestedToPersonId);
        }
    }
}
