using NSubstitute;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
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
                _invitationRepository.GetOrThrowAsync(new HouseGroupId(), Arg.Any<PersonId>()));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Returns_Invitation()
        {
            var invitation = InvitationProvider.Get();

            _invitationRepository.GetOrThrowAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>())
                .Returns(invitation);

            var returnedInvitation = await _invitationRepository.GetAsync(InvitationProvider.InvitationId, Guid.NewGuid());

            invitation.ShouldNotBeNull();
            returnedInvitation!.HouseGroupId.ShouldBe(invitation.HouseGroupId);
        }
    }
}
