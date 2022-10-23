using MediatR;
using NSubstitute;
using SharedHome.Application.Invitations.Commands.DeleteInvitation;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class DeleteInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly ICommandHandler<DeleteInvitationCommand, Unit> _commandHandler;

        public DeleteInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _commandHandler = new DeleteInvitationHandler(_invitationRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var invitation = InvitationProvider.Get();

            _invitationRepository.GetAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>())
                .Returns(invitation);

            var command = new DeleteInvitationCommand
            {
                HouseGroupId = InvitationProvider.HouseGroupId,
                PersonId = Guid.NewGuid()
            };

            await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).DeleteAsync(Arg.Any<Invitation>());
        }
    }
}
