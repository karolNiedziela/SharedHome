using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Enums;
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
        {
            var firstBill = Bill.Create(Guid.NewGuid(), InitializerConstants.CharlesUserId, BillType.Gas, "PGE", DateOnly.FromDateTime(new DateTime(2022, 1, 10)), new Money(1500m, "zł"));
            firstBill.CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            firstBill.ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            var secondBill = Bill.Create(Guid.NewGuid(), InitializerConstants.CharlesUserId, BillType.Water, "MPWIK", DateOnly.FromDateTime(new DateTime(2022, 1, 15)), new Money(250m, "zł"));
            secondBill.CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            secondBill.ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            var thirdBill = Bill.Create(Guid.NewGuid(), InitializerConstants.CharlesUserId, BillType.Electricity, "Tauron", DateOnly.FromDateTime(new DateTime(2022, 1, 20)), new Money(1000m, "zł"));
            thirdBill.CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            thirdBill.ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            var fourthBill = Bill.Create(Guid.NewGuid(), InitializerConstants.FrancUserId, BillType.Gas, "PGE", DateOnly.FromDateTime(new DateTime(2022, 1, 8)), new Money(2200m, "zł"));
            fourthBill.CreatedBy = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            fourthBill.ModifiedBy = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            var fifthBill = Bill.Create(Guid.NewGuid(), InitializerConstants.FrancUserId, BillType.Water, "MPWIK", DateOnly.FromDateTime(new DateTime(2022, 1, 5)), new Money(120m, "zł"));
            fifthBill.CreatedBy = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            fifthBill.ModifiedBy = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            var sixthBill = Bill.Create(Guid.NewGuid(), InitializerConstants.FrancUserId, BillType.Electricity, "Tauron", DateOnly.FromDateTime(new DateTime(2022, 1, 18)), new Money(750m, "zł"));
            sixthBill.CreatedBy = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            sixthBill.ModifiedBy = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";

            return new List<Bill> { firstBill, secondBill, thirdBill, fourthBill, fifthBill, sixthBill };
        }            
    }
}
