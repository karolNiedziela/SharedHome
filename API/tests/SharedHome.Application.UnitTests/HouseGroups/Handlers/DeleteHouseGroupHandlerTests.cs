using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Tests.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class DeleteHouseGroupHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly ICommandHandler<DeleteHouseGroupCommand, Response<Unit>> _commandHandler;

        public DeleteHouseGroupHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _commandHandler = new DeleteHouseGroupHandler(_houseGroupRepository, _invitationRepository);
        }
        
        [Fact]
        public async Task Handle_Should_Delete_House_Group_And_All_Invitations_To_This_House_Group_OnSuccess()
        {
            var houseGroup = HouseGroupProvider.Get();

            _houseGroupRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(houseGroup);

            var invitations = new List<Invitation>
            {
                Invitation.Create(houseGroup.Id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                Invitation.Create(houseGroup.Id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
            };

            _invitationRepository.GetAllAsync(Arg.Any<int>()).Returns(invitations);

            var command = new DeleteHouseGroupCommand
            {
                HouseGroupId = houseGroup.Id,
            };

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).DeleteAsync(Arg.Any<HouseGroup>());
            await _invitationRepository.Received(1).DeleteAsync(Arg.Any<IEnumerable<Invitation>>());
        }
    }
}
