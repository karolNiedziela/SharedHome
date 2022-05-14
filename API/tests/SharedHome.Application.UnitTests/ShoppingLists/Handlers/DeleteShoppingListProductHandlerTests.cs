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
    public class DeleteShoppingListProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IHouseGroupService _houseGroupService;
        private readonly ICommandHandler<DeleteShoppingListProduct, Unit> _commandHandler;

        public DeleteShoppingListProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _houseGroupService = Substitute.For<IHouseGroupService>();
            _commandHandler = new DeleteShoppingListProductHandler(_shoppingListRepository, _houseGroupService);
        }

        [Fact]
        public async Task DeleteShoppingListProduct_Should_Call_Repository_On_Success_WhenPersonIsInHouseGroup()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(true);

            var command = new DeleteShoppingListProduct
            {
                ShoppingListId = 0,
                ProductName = "Product"
            };

            _houseGroupService.GetShoppingListAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetWithProduct());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Any<ShoppingList>());
        }

        [Fact]
        public async Task DeleteShoppingListProduct_Should_Call_Repository_On_Success_WhenPersonIsNotInHouseGroup()
        {
            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(false);

            _shoppingListRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetWithProduct());

            var command = new DeleteShoppingListProduct
            {
                ShoppingListId = 0,
                ProductName = "Product"
            };

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Any<ShoppingList>());
        }
    }
}
