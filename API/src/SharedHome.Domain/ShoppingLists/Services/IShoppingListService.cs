using SharedHome.Domain.ShoppingLists.Aggregates;

namespace SharedHome.Domain.ShoppingLists.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingList> GetAsync(int shoppingListId, string personId);
    }
}
