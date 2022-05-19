using NSubstitute;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.ShoppingLists.Repositories;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Extensions
{
    public class ShoppingListRepositoryExtensions
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListRepositoryExtensions()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Should_Throw_ShoppingListNotFoundException_When_ShoppingList_Not_Found()
        {
            var exception = await Record.ExceptionAsync(() =>
                _shoppingListRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()));

            exception.ShouldBeOfType<ShoppingListNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Should_Return_Bill_When_Bill_Exists()
        {
            var shoppingList = ShoppingListProvider.GetEmpty();

            _shoppingListRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(shoppingList);

            var returnedShoppingList = await _shoppingListRepository.GetOrThrowAsync(1, "personId");

            returnedShoppingList.Name.Name.ShouldBe(ShoppingListProvider.DefaultShoppingListName);
        }
    }
}
