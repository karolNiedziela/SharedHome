﻿using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.UpdateShoppingListProduct;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class UpdateShoppingListProductHandler
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly ICommandHandler<UpdateShoppingListProductCommand, Unit> _commandHandler;

        public UpdateShoppingListProductHandler()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new UpdateShoppingListProductCommandHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new UpdateShoppingListProductCommand
            {
                CurrentProductName = ShoppingListProvider.ProductName,
                NewProductName = ShoppingListProvider.ProductName,
                Quantity = 2,
                NetContent = "300"
            };

            var shoppingList = ShoppingListProvider.GetWithProduct(netContent: new NetContent("500", NetContentType.g));

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(shoppingList);

            await _commandHandler.Handle(command, default!);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(
                x => x.Products.First().Quantity.Value == 2 &&
                x.Products.First().NetContent!.Value == "300"));
        }
    }
}