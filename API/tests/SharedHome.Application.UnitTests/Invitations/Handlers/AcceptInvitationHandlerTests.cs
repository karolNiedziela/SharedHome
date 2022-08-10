using MediatR;
using NSubstitute;
using SharedHome.Application.Invitations.Commands.AcceptInvitation;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class AcceptInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly ICommandHandler<AcceptInvitationCommand, Unit> _commandHandler;

        public AcceptInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new AcceptInvitationHandler(_invitationRepository, _houseGroupRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AcceptInvitationCommand
            {
                PersonId = "AcceptPersonId",
                HouseGroupId = 1,
            };

            var invitation = InvitationProvider.Get();

            _invitationRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(invitation);

            var houseGroup = HouseGroupProvider.Get();

            _houseGroupRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(houseGroup);

            await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).UpdateAsync(Arg.Is<Invitation>(invitation => 
                invitation.Status == InvitationStatus.Accepted));
            await _houseGroupRepository.Received(1).UpdateAsync(Arg.Any<HouseGroup>());
        }
    }
}
