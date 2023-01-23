using NSubstitute;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
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
                _shoppingListRepository.GetOrThrowAsync(Guid.NewGuid(), Arg.Any<PersonId>()));

            exception.ShouldBeOfType<ShoppingListNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Should_Return_Bill_When_Bill_Exists()
        {
            var shoppingList = ShoppingListFakeProvider.GetEmpty();

            _shoppingListRepository.GetOrThrowAsync(Arg.Any<ShoppingListId>(), Arg.Any<PersonId>())
                .Returns(shoppingList);

            var returnedShoppingList = await _shoppingListRepository.GetOrThrowAsync(ShoppingListFakeProvider.ShoppingListId, Guid.NewGuid());

            returnedShoppingList.Name.Name.ShouldBe(ShoppingListFakeProvider.ShoppingListName);
        }
    }
}
