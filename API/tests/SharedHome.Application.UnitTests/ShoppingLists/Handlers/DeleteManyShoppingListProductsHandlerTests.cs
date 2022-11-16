using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.DeleteManyShoppingListProducts;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

using SharedHome.Tests.Shared.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class DeleteManyShoppingListProductsHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly IRequestHandler<DeleteManyShoppingListProductsCommand, Unit> _commandHandler;

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
                ShoppingListId = ShoppingListProvider.ShoppingListId,
                ProductNames = new[] { "Produkt1", "Produkt3" }
            };

            var shoppingList = ShoppingListProvider.GetEmpty();
            shoppingList.AddProduct(ShoppingListProduct.Create("Produkt1", 1));
            shoppingList.AddProduct(ShoppingListProduct.Create("Produkt2", 1));
            shoppingList.AddProduct(ShoppingListProduct.Create("Produkt3", 1));

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(shoppingList);

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Any<ShoppingList>());
        }
    }
}
