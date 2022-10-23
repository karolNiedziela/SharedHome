using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.CancelPurchaseOfProduct;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class CancelPurchaseofProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<CancelPurchaseOfProductCommand, Unit> _commandHandler;

        public CancelPurchaseofProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();           
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new CancelPurchaseOfProductHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new CancelPurchaseOfProductCommand
            {
                ShoppingListId = ShoppingListProvider.ShoppingListId,
                ProductName = "Product"
            };

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(ShoppingListProvider.GetWithProduct(price: new Money(10m, "zł"), isBought: true));

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(
                shoppingList => shoppingList.Products.First().Price == null));
        }
    }
}
