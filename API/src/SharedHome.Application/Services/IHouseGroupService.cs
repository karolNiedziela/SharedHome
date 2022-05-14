using SharedHome.Domain.ShoppingLists.Aggregates;

namespace SharedHome.Application.Services
{
    public interface IHouseGroupService
    {
        Task<bool> IsPersonInHouseGroup(string personId);

        Task<bool> IsPersonInHouseGroup(string personId, int houseGroupId);

        Task<IEnumerable<string>> GetHouseGroupPersonsId(string personId);

        /// <summary>
        ///  Get shopping list for house group member.
        /// </summary>
        /// <param name="shoppingListId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        Task<ShoppingList> GetShoppingListAsync(int shoppingListId, string personId);

    }
}
