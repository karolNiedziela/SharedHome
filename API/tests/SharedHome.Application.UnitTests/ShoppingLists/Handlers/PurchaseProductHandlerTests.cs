using MediatR;
using NSubstitute;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.Commands.PurchaseProduct;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

using SharedHome.Tests.Shared.Providers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class PurchaseProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly IRequestHandler<PurchaseProductCommand, Unit> _commandHandler;

        public PurchaseProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new PurchaseProductHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new PurchaseProductCommand
            {
                ShoppingListId = ShoppingListProvider.ShoppingListId,
                ProductName = "Product",                
                Price = new MoneyDto(10, "zł")
            };

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(ShoppingListProvider.GetWithProduct());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList =>
                shoppingList.Products[0].Price!.Amount == 10));
        }
    }
}
