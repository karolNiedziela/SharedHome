using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.HandOwnerRoleOver;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;

using SharedHome.Tests.Shared.Providers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class HandOwnerRoleOverHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IRequestHandler<HandOwnerRoleOverCommand, Unit> _commandHandler;

        public HandOwnerRoleOverHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _commandHandler = new HandOwnerRoleOverHandler(_houseGroupRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var additionalMembers = new Guid[] { Guid.NewGuid()};

            var houseGroup = HouseGroupProvider.GetWithAdditionalMembers(additionalMembers);

            var command = new HandOwnerRoleOverCommand
            {
                HouseGroupId = Guid.NewGuid(),
                PersonId = HouseGroupProvider.PersonId,
                NewOwnerPersonId = additionalMembers[0]
            };

            _houseGroupRepository.GetAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>())
                .Returns(houseGroup);

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).UpdateAsync(Arg.Is<HouseGroup>(houseGroup => 
                houseGroup.Members.Where(member => member.PersonId.Value == HouseGroupProvider.PersonId)
                    .First().IsOwner == false &&
                houseGroup.Members.Where(member => member.PersonId.Value == additionalMembers[0])
                    .First().IsOwner == true));
        }
    }
}
