using MediatR;
using NSubstitute;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.Commands.PurchaseProducts;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

using SharedHome.Tests.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class PurchaseProductsHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly IRequestHandler<PurchaseProductsCommand, Unit> _commandHandler;

        public PurchaseProductsHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new PurchaseProductsHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new PurchaseProductsCommand
            {
                ShoppingListId = ShoppingListProvider.ShoppingListId,
                PriceByProductNames = new Dictionary<string, MoneyDto>
                {
                    { 
                        "Product",
                        new MoneyDto(50m, "zł")
                    },
                    {
                        "Product1",
                        new MoneyDto(25m, "zł")
                    },
                }
            };

            var shoppingList = ShoppingListProvider.GetEmpty();

            shoppingList.AddProducts(new[]
            {
                ShoppingListProduct.Create("Product", 1),
                ShoppingListProduct.Create("Product1", 1),
            });

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                    .Returns(shoppingList);

            await _commandHandler.Handle(command, default!);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList =>
              shoppingList.Products[0].IsBought == true &&
              shoppingList.Products[0].Price!.Amount == 50 &&
              shoppingList.Products[0].Price!.Currency.Value == "zł" &&
              shoppingList.Products[1].IsBought == true &&
              shoppingList.Products[1].Price!.Amount == 25 &&
              shoppingList.Products[1].Price!.Currency.Value == "zł"));
        }
    }
}
