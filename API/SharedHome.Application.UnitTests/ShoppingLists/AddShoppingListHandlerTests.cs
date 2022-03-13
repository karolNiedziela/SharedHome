using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Commands.Handlers;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists
{
    public class AddShoppingListHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly ICommandHandler<AddShoppingList, Unit> _commandHandler;

        public AddShoppingListHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _commandHandler = new AddShoppingListHandler(_shoppingListRepository);
        }


        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddShoppingList(Guid.NewGuid(), "ShoppingList");

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).AddAsync(Arg.Any<ShoppingList>());
        }
    }
}
