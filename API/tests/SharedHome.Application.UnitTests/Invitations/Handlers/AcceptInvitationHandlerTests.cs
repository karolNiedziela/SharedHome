using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Application.Invitations.Commands;
using SharedHome.Application.Invitations.Commands.Handlers;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class AcceptInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly ICommandHandler<AcceptInvitation, Unit> _commandHandler;

        public AcceptInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new AcceptInvitationHandler(_invitationRepository, _houseGroupRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AcceptInvitation
            {
                PersonId = "AcceptPersonId",
                HouseGroupId = 1,
                LastName = "personLastName",
                FirstName = "personFirstName",
                Email = "personEmail@email.com",
            };

            var invitation = InvitationProvider.Get();

            _invitationRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(invitation);

            var houseGroup = HouseGroupProvider.GetWithOwner();

            _houseGroupRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(houseGroup);

            await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).UpdateAsync(Arg.Is<Invitation>(invitation => 
                invitation.Status == InvitationStatus.Accepted));
            await _houseGroupRepository.Received(1).UpdateAsync(Arg.Any<HouseGroup>());
        }
    }
}
