using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly WriteSharedHomeDbContext _context;

        public BillRepository(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task<Bill?> GetAsync(int id, string personId)
            => await _context.Bills.SingleOrDefaultAsync(b => b.Id == id && b.PersonId == personId);

        public async Task<Bill?> GetAsync(int id, IEnumerable<string> personIds)
            => await _context.Bills
             .SingleOrDefaultAsync(bill => bill.Id == id &&
             personIds.Contains(bill.PersonId));

        public async Task AddAsync(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Bill bill)
        {
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bill bill)
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
        }
    }
}
