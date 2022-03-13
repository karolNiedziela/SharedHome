using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;

namespace SharedHome.Application.ShoppingLists.Extensions
{
    public static class ShoppingListRepositoryExtensions
    {
        public static async Task<ShoppingList> GetOrThrowAsync(this IShoppingListRepository shoppingListRepository, int shoppingListId)
        {
            var shoppingList = await shoppingListRepository.GetAsync(shoppingListId);
            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(shoppingListId);
            }

            return shoppingList;
        }
    }
}
