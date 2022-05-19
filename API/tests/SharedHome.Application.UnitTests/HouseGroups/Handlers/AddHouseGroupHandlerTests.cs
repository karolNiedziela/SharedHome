using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands;
using SharedHome.Application.HouseGroups.Commands.Handlers;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Services;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class AddHouseGroupHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly ICommandHandler<AddHouseGroup, Unit> _commandHandler;
        

        public AddHouseGroupHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _houseGroupService = Substitute.For<IHouseGroupReadService>();
            _commandHandler = new AddHouseGroupHandler(_houseGroupRepository, _houseGroupService);
        }

        [Fact]
        public async Task Handle_Should_Throw_PersonIsAlreadyInHouseGroupException_When_Person_Is_Already_In_HouseGroup()
        {
            var command = new AddHouseGroup
            {
                PersonId = "personId"
            };

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(true);

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PersonIsAlreadyInHouseGroupException>();
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddHouseGroup
            {
                PersonId = "personId",
                FirstName = "person",
                LastName = "person",
                Email = "person@email.com"
            };

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(false);

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).AddAsync(Arg.Is<HouseGroup>(houseGroup =>
            houseGroup.Members.First().PersonId == command.PersonId));
        }
    }
}
