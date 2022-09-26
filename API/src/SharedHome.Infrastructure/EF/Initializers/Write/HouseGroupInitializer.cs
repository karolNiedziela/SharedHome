using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Entities;
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

            var houseGroup = HouseGroup.Create("Default");
            houseGroup.CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            houseGroup.ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";

            var firstMember = new HouseGroupMember(houseGroup.Id, InitializerConstants.CharlesUserId, true);
            firstMember.CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            firstMember.ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            var secondMember = new HouseGroupMember(houseGroup.Id, InitializerConstants.FrancUserId, false);
            secondMember.CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            secondMember.ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";

            await _context.HouseGroups.AddAsync(houseGroup);
            await _context.SaveChangesAsync();
        }
    }
}
