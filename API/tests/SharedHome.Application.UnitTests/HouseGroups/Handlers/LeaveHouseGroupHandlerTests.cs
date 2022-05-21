using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands;
using SharedHome.Application.HouseGroups.Commands.Handlers;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class LeaveHouseGroupHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly ICommandHandler<LeaveHouseGroup, Unit> _commandHandler;

        public LeaveHouseGroupHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new LeaveHouseGroupHandler(_houseGroupRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Delete_Repository_Method_When_Members_Count_Is_Equal_To_Zero()
        {
            var houseGroup = HouseGroupProvider.GetWithOwner();

            _houseGroupRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(houseGroup);

            var command = new LeaveHouseGroup
            {
                HouseGroupId = HouseGroupProvider.DefaultHouseGroupId,
                PersonId = HouseGroupProvider.DefaultPersonId
            };

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).DeleteAsync(Arg.Any<HouseGroup>());
        }

        [Fact]
        public async Task Handle_Should_Call_Update_Repository_Method_When_Members_Count_Is_Greater_Than_Zero()
        {
            var houseGroup = HouseGroupProvider.GetWithAdditionalMembers();

            _houseGroupRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(houseGroup);

            var command = new LeaveHouseGroup
            {
                HouseGroupId = HouseGroupProvider.DefaultHouseGroupId,
                PersonId = HouseGroupProvider.DefaultPersonId,
                NewOwnerId = HouseGroupProvider.DefaultPersonId + 0
            };

        await _commandHandler.Handle(command, default);

        await _houseGroupRepository.Received(1).UpdateAsync(Arg.Any<HouseGroup>());
    }
    }
}
