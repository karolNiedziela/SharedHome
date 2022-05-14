using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Services
{
    public class HouseGroupService : IHouseGroupService
    {
        private readonly WriteSharedHomeDbContext _dbContext;

        public HouseGroupService(WriteSharedHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetHouseGroupPersonsId(string personId)
         => await _dbContext.HouseGroups
           .Include(houseGroup => houseGroup.Members
             .Where(member => member.PersonId == personId))
           .SelectMany(houseGroup => houseGroup.Members
           .Select(member => member.PersonId))
           .ToListAsync();

        public async Task<bool> IsPersonInHouseGroup(string personId)
          => await _dbContext.HouseGroups
            .Include(houseGroup => houseGroup.Members)
          .AnyAsync(houseGroup => houseGroup.Members
              .Any(member => member.PersonId == personId));

        public async Task<bool> IsPersonInHouseGroup(string personId, int houseGroupId)
            => await _dbContext.HouseGroups
            .AnyAsync(houseGroup => houseGroup.Id == houseGroupId && houseGroup.Members
                .Any(member => member.PersonId == personId));

        public async Task<ShoppingList> GetShoppingListAsync(int shoppingListId, string personId)
        {
            var houseGroupPersonIds = await GetHouseGroupPersonsId(personId);

            var shoppingList = await _dbContext.ShoppingLists
                .Include(shoppingList => shoppingList.Products)
                .SingleOrDefaultAsync(shoppingList => shoppingList.Id == shoppingListId && houseGroupPersonIds.Contains(shoppingList.PersonId!));

            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(shoppingListId);
            }

            return shoppingList;
        }

    }
}
