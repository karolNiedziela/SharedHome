using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Tests.Shared.Providers;
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
        public async Task Handle_Should_Delete_House_Group_And_All_Invitations_To_This_House_Group_OnSuccess()
        {
            var houseGroup = HouseGroupProvider.Get();

            _houseGroupRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(houseGroup);

           
            var command = new DeleteHouseGroupCommand
            {
                HouseGroupId = houseGroup.Id,
            };

            await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).DeleteAsync(Arg.Any<HouseGroup>());

        }
    }
}
