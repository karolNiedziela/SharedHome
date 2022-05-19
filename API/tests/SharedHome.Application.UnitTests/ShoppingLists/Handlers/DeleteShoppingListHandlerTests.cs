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
    public class DeleteShoppingListHandlerTests 
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<DeleteShoppingList, Unit> _commandHandler;

        public DeleteShoppingListHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new DeleteShoppingListHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task DeleteShoppingList_Should_Call_Repository_On_Success_WhenPersonIsInHouseGroup()
        {
            var command = new DeleteShoppingList
            {
                ShoppingListId = 0
            };

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).DeleteAsync(Arg.Any<ShoppingList>());
        }

        [Fact]
        public async Task DeleteShoppingList_Should_Call_Repository_On_Success_WhenPersonIsNotInHouseGroup()
        {
            _shoppingListRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetEmpty());

            var command = new DeleteShoppingList
            {
                ShoppingListId = 0
            };        

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).DeleteAsync(Arg.Any<ShoppingList>());
        }
    }
}
