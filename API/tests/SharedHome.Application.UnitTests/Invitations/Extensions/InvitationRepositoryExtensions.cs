using NSubstitute;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.Invitations.Repositories;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Extensions
{
    public class InvitationRepositoryExtensions
    {
        private readonly IInvitationRepository _invitationRepository;

        public InvitationRepositoryExtensions()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();

        }

        [Fact]
        public async Task GetOrThrowAsync_Throws_InvitationNotFoundException_When_Invitation_Does_Not_Exist()
        {
            var exception = await Record.ExceptionAsync(() =>
                _invitationRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Returns_Invitation()
        {
            var invitation = InvitationProvider.Get();

            _invitationRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(invitation);

            var returnedInvitation = await _invitationRepository.GetAsync(1, "personId");

            invitation.ShouldNotBeNull();
            returnedInvitation!.HouseGroupId.ShouldBe(1);
        }
    }
}
