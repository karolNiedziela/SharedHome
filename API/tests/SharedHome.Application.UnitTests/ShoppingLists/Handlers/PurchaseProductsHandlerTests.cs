using MediatR;
using NSubstitute;
using SharedHome.Application.Common.Models;
using SharedHome.Application.ShoppingLists.Commands.PurchaseProducts;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
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
        private readonly ICommandHandler<PurchaseProductsCommand, Unit> _commandHandler;

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
                ShoppingListId = 1,
                PriceByProductNames = new Dictionary<string, MoneyModel>
                {
                    { 
                        "Product",
                        new MoneyModel 
                        {
                            Price = 50m,
                            Currency = "zł"
                        }
                    },
                    {
                        "Product1",
                        new MoneyModel
                        {
                            Price = 25m,
                            Currency = "zł"
                        }
                    },
                }
            };

            var shoppingList = ShoppingListProvider.GetEmpty();

            shoppingList.AddProducts(new[]
            {
                new ShoppingListProduct("Product", 1),
                new ShoppingListProduct("Product1", 1),
            });

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                    .Returns(shoppingList);

            await _commandHandler.Handle(command, default!);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(shoppingList =>
              shoppingList.Products.First().IsBought == true &&
              shoppingList.Products.First().Price!.Amount == 50 &&
              shoppingList.Products.First().Price!.Currency.Value == "zł" &&
              shoppingList.Products.Last().IsBought == true &&
              shoppingList.Products.Last().Price!.Amount == 25 &&
              shoppingList.Products.Last().Price!.Currency.Value == "zł"));
        }
    }
}
