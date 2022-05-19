using SharedHome.Domain.ShoppingLists.Aggregates;

namespace SharedHome.Domain.ShoppingLists.Repositories
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList?> GetAsync(int id, string personId);

        Task<ShoppingList?> GetAsync(int id, IEnumerable<string> personIds);

        Task<ShoppingList> AddAsync(ShoppingList shoppingList);

        Task UpdateAsync(ShoppingList shoppingList);

        Task DeleteAsync(ShoppingList shoppingList);
    }
}
