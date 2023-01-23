using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

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
        private readonly IRequestHandler<SetIsShoppingListDoneCommand, Unit> _commandHandler;

        public SetIsShoppingListDoneHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new SetIsShoppingListDoneHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success_WhenPersonIsInHouseGroupAndIsDoneTrue()
        {
            var shoppingList = ShoppingListFakeProvider.GetEmpty();
            shoppingList.MarkAsDone();
            var command = new SetIsShoppingListDoneCommand
            {
                ShoppingListId = ShoppingListFakeProvider.ShoppingListId,
                Status = (int)ShoppingListStatus.Done,
            };

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(ShoppingListFakeProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.Status == ShoppingListStatus.Done));
        }

        [Fact]
        public async Task SetIsShoppingListDone_Should_Call_Repository_On_Success()
        {
            var shoppingList = ShoppingListFakeProvider.GetEmpty();
            shoppingList.MarkAsDone();
            var command = new SetIsShoppingListDoneCommand
            {
                ShoppingListId = ShoppingListFakeProvider.ShoppingListId,
                Status = (int)ShoppingListStatus.ToDo,
            };

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                            .Returns(shoppingList);


            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList
                => shoppingList.Status == ShoppingListStatus.ToDo));
        }

    }
}
