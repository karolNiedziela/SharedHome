using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Initializers.Write
{
    public class BillInitializer : IDataInitializer
    {
        private readonly WriteSharedHomeDbContext _context;

        public BillInitializer(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Bills.AsNoTracking().AnyAsync()) return;

            var bills = GetBills();
            await _context.Bills.AddRangeAsync(bills);
            await _context.SaveChangesAsync();
        }

        private List<Bill> GetBills()
            => new()
            {
                Bill.Create(InitializerConstants.CharlesUserId, BillType.Gas, "PGE", new DateTime(2022, 1, 10), 1500m),
                Bill.Create(InitializerConstants.CharlesUserId, BillType.Water, "MPWIK", new DateTime(2022, 1, 15), 250m),
                Bill.Create(InitializerConstants.CharlesUserId, BillType.Electricity, "Tauron", new DateTime(2022, 1, 20), 1000m),
                Bill.Create(InitializerConstants.FrancUserId, BillType.Gas, "PGE", new DateTime(2022, 1, 8), 2200m),
                Bill.Create(InitializerConstants.FrancUserId, BillType.Water, "MPWIK", new DateTime(2022, 1, 5), 120m),
                Bill.Create(InitializerConstants.FrancUserId, BillType.Electricity, "Tauron", new DateTime(2022, 1, 18), 750m),
            };
    }
}
