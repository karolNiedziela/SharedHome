using SharedHome.Domain.Bills;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;

namespace SharedHome.IntegrationTests.DataSeeds
{
    public class BillSeed
    {
        private readonly WriteSharedHomeDbContext _context;

        public BillSeed(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }
    }
}
