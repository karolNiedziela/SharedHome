using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Repositories
{
    internal sealed class ShoppingListRepository : IShoppingListRepository
    {
        private readonly WriteSharedHomeDbContext _context;

        public ShoppingListRepository(WriteSharedHomeDbContext context)
        {
            _context = context;
        }
        public async Task<ShoppingList?> GetAsync(int id, string personId)
        {
            var result = await _context.ShoppingLists
            .Include(shoppingList => shoppingList.Products)
            .SingleOrDefaultAsync(shoppingList => shoppingList.Id == id && shoppingList.PersonId == personId);

            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
            return result;
        }
        //=> await _context.ShoppingLists
        //.Include(shoppingList => shoppingList.Products)
        //.SingleOrDefaultAsync(shoppingList => shoppingList.Id == id && shoppingList.PersonId == personId);

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

            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

            await _context.SaveChangesAsync();
        }
    }
}
