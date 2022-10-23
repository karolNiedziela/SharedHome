using MediatR;
using NSubstitute;
using SharedHome.Application.Invitations.Commands.AcceptInvitation;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class AcceptInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly ICommandHandler<AcceptInvitationCommand, Unit> _commandHandler;

        public AcceptInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _houseGroupReadService = Substitute.For<IHouseGroupReadService>();
            _commandHandler = new AcceptInvitationHandler(_invitationRepository, _houseGroupRepository, _houseGroupReadService);
        }

        [Fact]
        public async Task Handle_Should_Throw_PersonIsAlreadyInHouseGroupException_If_Person_Is_Already_In_House_Group()
        {
            var command = new AcceptInvitationCommand
            {
                PersonId = InvitationProvider.RequestedByPersonId,
                HouseGroupId = InvitationProvider.HouseGroupId,
            };

            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<Guid>()).Returns(true);

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldBeOfType<PersonIsAlreadyInHouseGroupException>();
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AcceptInvitationCommand
            {
                PersonId = InvitationProvider.RequestedByPersonId,
                HouseGroupId = InvitationProvider.HouseGroupId,
            };

            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<Guid>()).Returns(false);

            var invitation = InvitationProvider.Get();

            _invitationRepository.GetAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>())
                .Returns(invitation);

            var houseGroup = HouseGroupProvider.Get();

            _houseGroupRepository.GetAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>())
                .Returns(houseGroup);

            await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).UpdateAsync(Arg.Is<Invitation>(invitation => 
                invitation.Status == InvitationStatus.Accepted));
            await _houseGroupRepository.Received(1).UpdateAsync(Arg.Any<HouseGroup>());
        }
    }
}
