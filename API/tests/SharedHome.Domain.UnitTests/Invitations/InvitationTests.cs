using SharedHome.Domain.Invitations;
using SharedHome.Domain.Invitations.Enums;
using SharedHome.Domain.Invitations.Exceptions;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using Xunit;

namespace SharedHome.Domain.UnitTests.Invitations
{
    public class InvitationTests
    {
        [Fact]
        public void NewInvitation_Should_Have_InvitationStatus_Set_To_Pending()
        {
            var invitation = InvitationProvider.Get();

            invitation.Status.ShouldBe(InvitationStatus.Pending);
        }

        [Fact]
        public void Accept_Throws_InvitationAcceptedException_When_Invitation_Is_Already_Accepted()
        {
            Invitation invitation = InvitationProvider.Get();
            invitation.Accept();

            var exception = Record.Exception(() => invitation.Accept());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationAcceptedException>();
        }

        [Fact]
        public void Accept_Throws_InvitationRejectedException_When_Invitation_Is_Already_Rejected()
        {
            Invitation invitation = InvitationProvider.Get();
            invitation.Reject();

            var exception = Record.Exception(() => invitation.Accept());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationRejectedException>();
        }

        [Fact]
        public void Accept_Should_Set_InvitationStatus_To_Accepted()
        {
            var invitation = InvitationProvider.Get();

            invitation.Accept();

            invitation.Status.ShouldBe(InvitationStatus.Accepted);
        }

        [Fact]
        public void Reject_Throws_InvitationAcceptedException_When_Invitation_Is_Already_Accepted()
        {
            Invitation invitation = InvitationProvider.Get();
            invitation.Accept();

            var exception = Record.Exception(() => invitation.Reject());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationAcceptedException>();
        }

        [Fact]
        public void Reject_Throws_InvitationRejectedException_When_Invitation_Is_Already_Rejected()
        {
            Invitation invitation = InvitationProvider.Get();
            invitation.Reject();

            var exception = Record.Exception(() => invitation.Reject());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationRejectedException>();
        }

        [Fact]
        public void Reject_Should_Set_InvitationStatus_To_Rejected()
        {
            var invitation = InvitationProvider.Get();

            invitation.Reject();

            invitation.Status.ShouldBe(InvitationStatus.Rejected);
        }
    }
}
