using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.LeaveHouseGroup;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;

using SharedHome.Tests.Shared.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class LeaveHouseGroupHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IRequestHandler<LeaveHouseGroupCommand, Unit> _commandHandler;

        public LeaveHouseGroupHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new LeaveHouseGroupHandler(_houseGroupRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Delete_Repository_Method_When_Members_Count_Is_Equal_To_Zero()
        {
            var houseGroup = HouseGroupFakeProvider.GetWithMember();

            _houseGroupRepository.GetOrThrowAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>()).Returns(houseGroup);

            var command = new LeaveHouseGroupCommand
            {
                HouseGroupId = HouseGroupFakeProvider.HouseGroupId,
                PersonId = HouseGroupFakeProvider.PersonId
            };

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).DeleteAsync(Arg.Any<HouseGroup>());
        }

        [Fact]
        public async Task Handle_Should_Call_Update_Repository_Method_When_Members_Count_Is_Greater_Than_Zero()
        {
            var houseGroup = HouseGroupFakeProvider.GetWithAdditionalMembers(new Guid[] { Guid.NewGuid() });

            _houseGroupRepository.GetOrThrowAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>()).Returns(houseGroup);

            var command = new LeaveHouseGroupCommand
            {
                HouseGroupId = HouseGroupFakeProvider.HouseGroupId,
                PersonId = HouseGroupFakeProvider.PersonId,
            };

        await _commandHandler.Handle(command, default);

        await _houseGroupRepository.Received(1).UpdateAsync(Arg.Any<HouseGroup>());
    }
    }
}
