using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class DeleteHouseGroupHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly ICommandHandler<DeleteHouseGroupCommand, Response<Unit>> _commandHandler;

        public DeleteHouseGroupHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new DeleteHouseGroupHandler(_houseGroupRepository);
        }
        
        [Fact]
        public async Task Should_Throw_HouseGroupMemberIsNotOwnerException_When_Member_Is_Not_Owner()
        {
            var houseGroup = HouseGroupProvider.GetWithMember(false);

            _houseGroupRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(houseGroup);

            var command = new DeleteHouseGroupCommand
            {
                HouseGroupId = houseGroup.Id,
                PersonId = houseGroup.Members.First().PersonId
            };

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldBeOfType<HouseGroupMemberIsNotOwnerException>();
        }

        [Fact]
        public async Task Handle_Should_Delete_House_Group_When_Member_IsOwner()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            _houseGroupRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(houseGroup);
           
            var command = new DeleteHouseGroupCommand
            {
                HouseGroupId = houseGroup.Id,
                PersonId = houseGroup.Members.First().PersonId
            };

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).DeleteAsync(Arg.Any<HouseGroup>());

        }
    }
}
