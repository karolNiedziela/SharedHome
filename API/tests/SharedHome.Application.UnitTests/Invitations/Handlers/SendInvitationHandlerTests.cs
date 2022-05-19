using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Commands;
using SharedHome.Application.Invitations.Commands.Handlers;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Services;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Constants;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class SendInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IInvitationService _invitationService;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly ICommandHandler<SendInvitation, Unit> _commandHandler;

        public SendInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _houseGroupService = Substitute.For<IHouseGroupReadService>();
            _invitationService = Substitute.For<IInvitationService>();
            _commandHandler = new SendInvitationHandler(_invitationRepository, _houseGroupService, _invitationService);
        }

        [Fact]
        public async Task Handle_Throws_PersonIsNotInHouseGroupException_When_Person_Is_Not_In_HouseGroup()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>(), Arg.Any<int>())
                .Returns(false);

            var exception = await Record.ExceptionAsync(() => 
                _commandHandler.Handle(new SendInvitation(), default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PersonIsNotInHouseGroupException>();
        }

        [Fact]
        public async Task Handle_Throws_InvitationAlreadySentException_When_Invitation_Already_Sent_ToPerson()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>(), Arg.Any<int>())
                .Returns(true);

            _invitationService.IsAnyInvitationFromHouseGroupToPerson(Arg.Any<int>(), Arg.Any<string>())
                .Returns(true);

            var exception = await Record.ExceptionAsync(() =>
                _commandHandler.Handle(new SendInvitation(), default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationAlreadySentException>();
        }

        [Fact]
        public async Task Handle_Shoudl_Call_Repository_OnSuccess()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>(), Arg.Any<int>())
             .Returns(true);

            _invitationService.IsAnyInvitationFromHouseGroupToPerson(Arg.Any<int>(), Arg.Any<string>())
                .Returns(false);

            var command = new SendInvitation
            {
                HouseGroupId = 1,
                PersonId = "personId",
                RequestedToPersonId = "requestedPersonId",
                FirstName = "FirstName",
                LastName = "LastName",
            };

            await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).AddAsync(Arg.Is<Invitation>(invitation =>
                invitation.Status == InvitationStatus.Pending));
        }
    }
}
