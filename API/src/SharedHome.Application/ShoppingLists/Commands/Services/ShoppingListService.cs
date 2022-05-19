using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

namespace SharedHome.Application.ShoppingLists.Commands.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListService(IShoppingListRepository shoppingListRepository, IHouseGroupReadService houseGroupReadService)
        {
            _shoppingListRepository = shoppingListRepository;
            _houseGroupReadService = houseGroupReadService;
        }

        public async Task<ShoppingList> GetAsync(int shoppingListId, string personId)
        {
            if (await _houseGroupReadService.IsPersonInHouseGroup(personId))
            {
                var houseGroupPersonIds = await _houseGroupReadService.GetMemberPersonIds(personId);

                return await _shoppingListRepository.GetOrThrowAsync(shoppingListId, houseGroupPersonIds);           
            }

            return await _shoppingListRepository.GetOrThrowAsync(shoppingListId, personId);
        }
    }
}
