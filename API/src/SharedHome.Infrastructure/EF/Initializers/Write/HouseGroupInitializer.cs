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
            houseGroup.CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            houseGroup.CreatedBy = InitializerConstants.CharlesUserId;

            var firstMember = new HouseGroupMember(houseGroup.Id, InitializerConstants.CharlesUserId, true)
            {
                CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}",
                CreatedBy = InitializerConstants.CharlesUserId
            };
            var secondMember = new HouseGroupMember(houseGroup.Id, InitializerConstants.FrancUserId, false)
            {
                CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}",
                CreatedBy = InitializerConstants.CharlesUserId
            };

            houseGroup.AddMember(firstMember);
            houseGroup.AddMember(secondMember);
            houseGroup.ClearEvents();
            await _context.HouseGroups.AddAsync(houseGroup);
            await _context.SaveChangesAsync();
        }
    }
}
