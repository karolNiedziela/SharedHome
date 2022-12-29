using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;

namespace SharedHome.Application.ShoppingLists.Services
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

        public async Task<ShoppingList> GetAsync(Guid shoppingListId, Guid personId)
        {
            if (await _houseGroupReadService.IsPersonInHouseGroupAsync(personId))
            {
                var houseGroupPersonIds = await _houseGroupReadService.GetMemberPersonIdsAsync(personId);

                var convertedHouseGroupPersonIds = new List<PersonId>(houseGroupPersonIds.Select(x => new PersonId(x)));

                return await _shoppingListRepository.GetOrThrowAsync(shoppingListId, convertedHouseGroupPersonIds);           
            }

            return await _shoppingListRepository.GetOrThrowAsync(shoppingListId, personId);
        }
    }
}
