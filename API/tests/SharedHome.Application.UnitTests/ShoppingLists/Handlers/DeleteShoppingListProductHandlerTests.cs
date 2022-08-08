using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.DeleteShoppingListProduct;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class DeleteShoppingListProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<DeleteShoppingListProductCommand, Unit> _commandHandler;

        public DeleteShoppingListProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new DeleteShoppingListProductHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task DeleteShoppingListProduct_Should_Call_Repository_On_Success()
        {
            var command = new DeleteShoppingListProductCommand
            {
                ShoppingListId = 0,
                ProductName = "Product"
            };

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetWithProduct());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Any<ShoppingList>());
        }
    }
}
