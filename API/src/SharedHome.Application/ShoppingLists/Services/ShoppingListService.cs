using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

namespace SharedHome.Application.ShoppingLists.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListService(IShoppingListRepository shoppingListRepository, IHouseGroupRepository houseGroupRepository)
        {
            _shoppingListRepository = shoppingListRepository;
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<ShoppingList> GetAsync(Guid shoppingListId, Guid personId)
        {
            if (await _houseGroupRepository.IsPersonInHouseGroupAsync(personId))
            {
                var houseGroupPersonIds = await _houseGroupRepository.GetMemberPersonIdsAsync(personId);

                return await _shoppingListRepository.GetOrThrowAsync(shoppingListId, houseGroupPersonIds);           
            }

            return await _shoppingListRepository.GetOrThrowAsync(shoppingListId, personId);
        }
    }
}
