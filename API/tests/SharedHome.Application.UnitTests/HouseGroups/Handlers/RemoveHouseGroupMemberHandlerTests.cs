﻿using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.RemoveHouseGroupMember;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;

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
        private readonly IRequestHandler<RemoveHouseGroupMemberCommand, Unit> _commandHandler;

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
            var houseGroup = HouseGroupFakeProvider.GetWithAdditionalMembers(additionalMembers);

            var command = new RemoveHouseGroupMemberCommand
            {
                HouseGroupId = HouseGroupFakeProvider.HouseGroupId,
                PersonId = HouseGroupFakeProvider.PersonId,
                PersonToRemoveId = personToRemoveId
            };

            _houseGroupRepository.GetAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>())
                .Returns(houseGroup);

            var membersCountBeforeRemove = houseGroup.Members.Count();

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).UpdateAsync(Arg.Any<HouseGroup>());

            membersCountBeforeRemove.ShouldBe(2);
            houseGroup.Members.Count().ShouldBe(1);
        }
    }
}
