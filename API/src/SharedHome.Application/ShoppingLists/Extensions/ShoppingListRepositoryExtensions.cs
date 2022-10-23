using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Application.ShoppingLists.Extensions
{
    public static class ShoppingListRepositoryExtensions
    {
        public static async Task<ShoppingList> GetOrThrowAsync(this IShoppingListRepository shoppingListRepository,
            Guid shoppingListId, Guid personId)
        {
            var shoppingList = await shoppingListRepository.GetAsync(shoppingListId, personId);
            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(shoppingListId);
            }

            return shoppingList;
        }

        public static async Task<ShoppingList> GetOrThrowAsync(this IShoppingListRepository shoppingListRepository, 
            Guid shoppingListId, IEnumerable<Guid> personIds)
        {
            var personIdsConverted = new List<PersonId>();
            foreach (var personId in personIds)
            {
                personIdsConverted.Add(personId);
            }

            var shoppingList = await shoppingListRepository.GetAsync(shoppingListId, personIdsConverted);
            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(shoppingListId);
            }

            return shoppingList;
        }

    }
}
