using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class SetIsShoppingListDoneHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<SetIsShoppingListDoneCommand, Unit> _commandHandler;

        public SetIsShoppingListDoneHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new SetIsShoppingListDoneHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success_WhenPersonIsInHouseGroupAndIsDoneTrue()
        {            
            var command = new SetIsShoppingListDoneCommand
            {
                ShoppingListId = ShoppingListProvider.ShoppingListId,
                IsDone = true,
            };

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(ShoppingListProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == true));
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success()
        {
            var command = new SetIsShoppingListDoneCommand
            {
                ShoppingListId = ShoppingListProvider.ShoppingListId,
                IsDone = false,
            };

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                            .Returns(ShoppingListProvider.GetEmpty(true));

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.IsDone == false));
        }

    }
}
