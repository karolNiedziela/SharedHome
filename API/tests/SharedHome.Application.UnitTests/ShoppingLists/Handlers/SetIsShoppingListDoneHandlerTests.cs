using MediatR;
using NSubstitute;
using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Commands.Handlers;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class SetIsShoppingListDoneHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IHouseGroupService _houseGroupService;
        private readonly ICommandHandler<SetIsShoppingListDone, Unit> _commandHandler;

        public SetIsShoppingListDoneHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _houseGroupService = Substitute.For<IHouseGroupService>();
            _commandHandler = new SetIsShoppingListDoneHandler(_shoppingListRepository, _houseGroupService);
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success_WhenPersonIsInHouseGroupAndIsDoneTrue()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(true);

            var command = new SetIsShoppingListDone
            {
                ShoppingListId = 0,
                IsDone = true,
            };

            _houseGroupService.GetShoppingListAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == true));
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success_WhenPersonIsInHouseGroupAndIsDoneFalse()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(true);

            var command = new SetIsShoppingListDone
            {
                ShoppingListId = 0,
                IsDone = false,
            };

            _houseGroupService.GetShoppingListAsync(Arg.Any<int>(), Arg.Any<string>())
                            .Returns(ShoppingListProvider.GetEmpty(true));

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == false));
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success_WhenPersonIsNotInHouseGroupAndIsDoneTrue()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(false);

            var command = new SetIsShoppingListDone
            {
                ShoppingListId = 0,
                IsDone = true,
            };

            _shoppingListRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == true));
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success_WhenPersonIsNotInHouseGroupAndIsDoneFalse()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(false);

            var command = new SetIsShoppingListDone
            {
                ShoppingListId = 0,
                IsDone = false,
            };

            _shoppingListRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
               .Returns(ShoppingListProvider.GetEmpty(true));

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == false));
        }
    }
}
