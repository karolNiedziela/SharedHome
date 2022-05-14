using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.ValueObjects;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Initializers.Write
{
    public class HouseGroupInitializer : IDataInitializer
    {
        private readonly WriteSharedHomeDbContext _context;

        public HouseGroupInitializer(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.HouseGroups.AnyAsync()) return;

            var houseGroup = HouseGroup.Create();

            await _context.HouseGroups.AddAsync(houseGroup);
            await _context.SaveChangesAsync();

            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, InitializerConstants.CharlesUserId, true));
            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, InitializerConstants.FrancUserId, false));

            _context.HouseGroups.Update(houseGroup);
            await _context.SaveChangesAsync();
        }
    }
}
