using MediatR;
using NSubstitute;
using SharedHome.Application.Invitations.Commands;
using SharedHome.Application.Invitations.Commands.Handlers;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class DeleteInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly ICommandHandler<DeleteInvitation, Unit> _commandHandler;

        public DeleteInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _commandHandler = new DeleteInvitationHandler(_invitationRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var invitation = InvitationProvider.Get();

            _invitationRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(invitation);

            var command = new DeleteInvitation
            {
                HouseGroupId = 1,
                PersonId = "personId"
            };

            await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).DeleteAsync(Arg.Any<Invitation>());
        }
    }
}
