using SharedHome.Domain.ShoppingLists;
using SharedHome.Infrastructure.EF.Contexts;
using System.Threading.Tasks;

namespace SharedHome.IntegrationTests.DataSeeds
{
    public class ShoppingListSeed
    {
        private readonly WriteSharedHomeDbContext _context;

        public ShoppingListSeed(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShoppingList shoppingList)
        {
            await _context.ShoppingLists.AddAsync(shoppingList);
            await _context.SaveChangesAsync();
        }
    }
}
