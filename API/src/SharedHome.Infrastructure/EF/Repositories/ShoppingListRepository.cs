using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Repositories
{
    internal sealed class ShoppingListRepository : IShoppingListRepository
    {
        private readonly SharedHomeDbContext _context;

        public ShoppingListRepository(SharedHomeDbContext context)
        {
            _context = context;
        }
        public async Task<ShoppingList?> GetAsync(int id, string personId)
            => await _context.ShoppingLists
            .Include(shoppingList => shoppingList.Products)
            .SingleOrDefaultAsync(shoppingList => shoppingList.Id == id && shoppingList.PersonId == personId);

        public async Task<ShoppingList> AddAsync(ShoppingList shoppingList)
        {
            await _context.ShoppingLists.AddAsync(shoppingList);

            await _context.SaveChangesAsync();

            return shoppingList;
        }

        public async Task DeleteAsync(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Remove(shoppingList);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Update(shoppingList);

            await _context.SaveChangesAsync();
        }
    }
}
