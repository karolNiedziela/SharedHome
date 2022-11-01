namespace SharedHome.Domain.ShoppingLists.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingList> GetAsync(Guid shoppingListId, Guid personId);
    }
}
