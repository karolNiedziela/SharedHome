﻿using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.UpdateShoppingList;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

using SharedHome.Tests.Shared.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class UpdateShoppingListHandlerTests 
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly IRequestHandler<UpdateShoppingListCommand, Unit> _commandHandler;

        public UpdateShoppingListHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            _commandHandler = new UpdateShoppingListHandler(_shoppingListRepository, _shoppingListService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var shoppingList = ShoppingListFakeProvider.GetEmpty();

            var command = new UpdateShoppingListCommand
            {
                Name = "TestName"
            };

            _shoppingListService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(shoppingList);

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Is<ShoppingList>(x =>
                x.Name.Name == "TestName"));
        }
    }
}
