using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.DeleteManyShoppingListProducts;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class DeleteManyShoppingListProductsHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<DeleteManyShoppingListProductsCommand, Unit> _commandHandler;

        public DeleteManyShoppingListProductsHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new DeleteManyShoppingListProductsHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task DeleteShoppingListProduct_Should_Call_Repository_On_Success()
        {
            var command = new DeleteManyShoppingListProductsCommand
            {
                ShoppingListId = 0,
                ProductNames = new[] { "Produkt1", "Produkt3" }
            };

            var shoppingList = ShoppingListProvider.GetEmpty();
            shoppingList.AddProduct(new ShoppingListProduct("Produkt1", 1));
            shoppingList.AddProduct(new ShoppingListProduct("Produkt2", 1));
            shoppingList.AddProduct(new ShoppingListProduct("Produkt3", 1));

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(shoppingList);

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Any<ShoppingList>());
        }
    }
}
