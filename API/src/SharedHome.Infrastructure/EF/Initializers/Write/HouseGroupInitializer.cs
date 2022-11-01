using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.HouseGroups;
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

            var houseGroup = HouseGroup.Create(Guid.NewGuid(), "Default");
            houseGroup.CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            houseGroup.ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";

            var firstMember = new HouseGroupMember(houseGroup.Id, InitializerConstants.CharlesUserId, true)
            {
                CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}",
                ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}"
            };
            var secondMember = new HouseGroupMember(houseGroup.Id, InitializerConstants.FrancUserId, false)
            {
                CreatedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}",
                ModifiedBy = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}"
            };

            houseGroup.AddMember(firstMember);
            houseGroup.AddMember(secondMember);
            await _context.HouseGroups.AddAsync(houseGroup);
            await _context.SaveChangesAsync();
        }
    }
}
