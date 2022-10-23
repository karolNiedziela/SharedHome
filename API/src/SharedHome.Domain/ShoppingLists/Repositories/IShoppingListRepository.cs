using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Domain.ShoppingLists.Repositories
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList?> GetAsync(ShoppingListId id, PersonId personId);

        Task<ShoppingList?> GetAsync(ShoppingListId id, IEnumerable<PersonId> personIds);

        Task AddAsync(ShoppingList shoppingList);

        Task UpdateAsync(ShoppingList shoppingList);

        Task DeleteAsync(ShoppingList shoppingList);
    }
}
