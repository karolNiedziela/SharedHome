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
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class CancelPurchaseofProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IHouseGroupService _houseGroupService;
        private readonly ICommandHandler<CancelPurchaseOfProduct, Unit> _commandHandler;

        public CancelPurchaseofProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _houseGroupService = Substitute.For<IHouseGroupService>();
            _commandHandler = new CancelPurchaseOfProductHandler(_shoppingListRepository, _houseGroupService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new CancelPurchaseOfProduct
            {
                ShoppingListId = 1,
                ProductName = "Product"
            };

            _shoppingListRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(ShoppingListProvider.GetWithProduct(productPrice: 10, isBought: true));

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(
                shoppingList => shoppingList.Products.First().Price == null));
        }
    }
}
