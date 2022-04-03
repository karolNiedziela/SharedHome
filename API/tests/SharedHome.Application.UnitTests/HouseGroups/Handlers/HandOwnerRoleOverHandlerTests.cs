﻿using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands;
using SharedHome.Application.HouseGroups.Commands.Handlers;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class HandOwnerRoleOverHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly ICommandHandler<HandOwnerRoleOver, Unit> _commandHandler;

        public HandOwnerRoleOverHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new HandOwnerRoleOverHandler(_houseGroupRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var houseGroup = HouseGroupProvider.GetWithAdditionalMembers();

            var command = new HandOwnerRoleOver
            {
                HouseGroupId = 1,
                PersonId = "personId",
                NewOwnerPersonId = "personId0"
            };

            _houseGroupRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(houseGroup);

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).UpdateAsync(Arg.Is<HouseGroup>(houseGroup => 
                houseGroup.Members.Where(member => member.PersonId == "personId")
                    .First().IsOwner == false &&
                houseGroup.Members.Where(member => member.PersonId == "personId0")
                    .First().IsOwner == true));
        }
    }
}
