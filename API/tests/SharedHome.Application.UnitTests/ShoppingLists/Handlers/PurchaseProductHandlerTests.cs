using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Commands.Handlers;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class PurchaseProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<PurchaseProduct, Unit> _commandHandler;

        public PurchaseProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new PurchaseProductHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new PurchaseProduct
            {
                ShoppingListId = 1,
                ProductName = "Product",
                Price = 10,
                Currency = "zł"
            };

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(ShoppingListProvider.GetWithProduct());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList =>
                shoppingList.Products.First().Price!.Amount == 10));
        }
    }
}
