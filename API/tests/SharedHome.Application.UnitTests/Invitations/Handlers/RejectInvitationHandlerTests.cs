using MediatR;
using NSubstitute;
using SharedHome.Application.Invitations.Commands.RejectInvitation;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class RejectInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly ICommandHandler<RejectInvitationCommand, Unit> _commandHandler;

        public RejectInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _commandHandler = new RejectInvitationHandler(_invitationRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var invitation = InvitationProvider.Get();

            _invitationRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(invitation);

            var command = new RejectInvitationCommand
            {
                HouseGroupId = 1,
                PersonId = "personId"
            };

            await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).UpdateAsync(Arg.Is<Invitation>(invitation =>
                invitation.Status == InvitationStatus.Rejected));
        }
    }
}
