using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.RemoveHouseGroupMember;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class RemoveHouseGroupMemberHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly ICommandHandler<RemoveHouseGroupMemberCommand, Unit> _commandHandler;

        public RemoveHouseGroupMemberHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new RemoveHouseGroupMemberHandler(_houseGroupRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var personToRemoveId = Guid.NewGuid();
            var additionalMembers = new Guid[] { personToRemoveId };
            var houseGroup = HouseGroupProvider.GetWithAdditionalMembers(additionalMembers);

            var command = new RemoveHouseGroupMemberCommand
            {
                HouseGroupId = HouseGroupProvider.HouseGroupId,
                PersonId = HouseGroupProvider.PersonId,
                PersonToRemoveId = personToRemoveId
            };

            _houseGroupRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(houseGroup);

            var membersCountBeforeRemove = houseGroup.Members.Count();

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).UpdateAsync(Arg.Any<HouseGroup>());

            membersCountBeforeRemove.ShouldBe(2);
            houseGroup.Members.Count().ShouldBe(1);
        }
    }
}
