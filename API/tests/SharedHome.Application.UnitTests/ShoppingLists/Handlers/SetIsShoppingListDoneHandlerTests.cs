using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Commands.Handlers;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class SetIsShoppingListDoneHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<SetIsShoppingListDone, Unit> _commandHandler;

        public SetIsShoppingListDoneHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new SetIsShoppingListDoneHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success_WhenPersonIsInHouseGroupAndIsDoneTrue()
        {            
            var command = new SetIsShoppingListDone
            {
                ShoppingListId = 0,
                IsDone = true,
            };

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == true));
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success()
        {
            var command = new SetIsShoppingListDone
            {
                ShoppingListId = 0,
                IsDone = false,
            };

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                            .Returns(ShoppingListProvider.GetEmpty(true));

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == false));
        }

    }
}
