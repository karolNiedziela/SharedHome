using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Commands.Handlers;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class AddShoppingListProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly ICommandHandler<AddShoppingListProduct, Unit> _commandHandler;

        public AddShoppingListProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _commandHandler = new AddShoppingListProductHandler(_shoppingListRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddShoppingListProduct
            {
                ShoppingListId = 1,
                ProductName = "Product",
                Quantity = 1,
            };

            _shoppingListRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(ShoppingListProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Any<ShoppingList>());
        }

        [Fact]
        public async Task Handle_Should_Throw_ShoppingListNotFoundException_When_ShoppingList_Was_Not_Found()
        {
            var command = new AddShoppingListProduct
            {
                ShoppingListId = 1,
                ProductName = "Product",
                Quantity = 1,
            };

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ShoppingListNotFoundException>();
        }
    }
}
