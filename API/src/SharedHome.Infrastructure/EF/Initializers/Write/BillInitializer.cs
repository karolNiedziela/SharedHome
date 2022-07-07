using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
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

        private static List<Bill> GetBills()
            => new()
            {
                Bill.Create(InitializerConstants.CharlesUserId, BillType.Gas, "PGE", new DateTime(2022, 1, 10), new Money(1500m, "PLN")),
                Bill.Create(InitializerConstants.CharlesUserId, BillType.Water, "MPWIK", new DateTime(2022, 1, 15), new Money(250m, "PLN")),
                Bill.Create(InitializerConstants.CharlesUserId, BillType.Electricity, "Tauron", new DateTime(2022, 1, 20), new Money(1000m, "PLN")),
                Bill.Create(InitializerConstants.FrancUserId, BillType.Gas, "PGE", new DateTime(2022, 1, 8), new Money(2200m, "PLN")),
                Bill.Create(InitializerConstants.FrancUserId, BillType.Water, "MPWIK", new DateTime(2022, 1, 5), new Money(120m, "PLN")),
                Bill.Create(InitializerConstants.FrancUserId, BillType.Electricity, "Tauron", new DateTime(2022, 1, 18), new Money(750m, "PLN")),
            };
    }
}
