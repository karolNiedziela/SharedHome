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
            bills.ForEach(bill => bill.ClearEvents());
            await _context.Bills.AddRangeAsync(bills);
            await _context.SaveChangesAsync();
        }

        private static List<Bill> GetBills()
        {
            var firstBill = Bill.Create(Guid.NewGuid(), InitializerConstants.CharlesUserId, BillType.Gas, "PGE", DateOnly.FromDateTime(new DateTime(2022, 1, 10)), new Money(1500m, "zł"));
            firstBill.CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            firstBill.CreatedBy = InitializerConstants.CharlesUserId;
            var secondBill = Bill.Create(Guid.NewGuid(), InitializerConstants.CharlesUserId, BillType.Water, "MPWIK", DateOnly.FromDateTime(new DateTime(2022, 1, 15)), new Money(250m, "zł"));
            secondBill.CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            secondBill.CreatedBy = InitializerConstants.CharlesUserId;
            var thirdBill = Bill.Create(Guid.NewGuid(), InitializerConstants.CharlesUserId, BillType.ElectricCurrent, "Tauron", DateOnly.FromDateTime(new DateTime(2022, 1, 20)), new Money(1000m, "zł"));
            thirdBill.CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            secondBill.CreatedBy = InitializerConstants.CharlesUserId;
            var fourthBill = Bill.Create(Guid.NewGuid(), InitializerConstants.FrancUserId, BillType.Gas, "PGE", DateOnly.FromDateTime(new DateTime(2022, 1, 8)), new Money(2200m, "zł"));
            fourthBill.CreatedByFullName = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            fourthBill.CreatedBy = InitializerConstants.FrancUserId;
            var fifthBill = Bill.Create(Guid.NewGuid(), InitializerConstants.FrancUserId, BillType.Water, "MPWIK", DateOnly.FromDateTime(new DateTime(2022, 1, 5)), new Money(120m, "zł"));
            fifthBill.CreatedByFullName = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            fifthBill.CreatedBy = InitializerConstants.FrancUserId;
            var sixthBill = Bill.Create(Guid.NewGuid(), InitializerConstants.FrancUserId, BillType.ElectricCurrent, "Tauron", DateOnly.FromDateTime(new DateTime(2022, 1, 18)), new Money(750m, "zł"));
            sixthBill.CreatedByFullName = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            sixthBill.CreatedBy = InitializerConstants.FrancUserId;

            return new List<Bill> { firstBill, secondBill, thirdBill, fourthBill, fifthBill, sixthBill };
        }            
    }
}
