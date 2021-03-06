using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Initializers.Write
{
    public class PersonInitializer : IDataInitializer
    {
        private readonly WriteSharedHomeDbContext _context;

        public PersonInitializer(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Persons.AnyAsync()) return;

            var charles = Person.Create(InitializerConstants.CharlesUserId, InitializerConstants.CharlesFirstName, InitializerConstants.CharlesLastName);
            await _context.Persons.AddAsync(charles);

            var franc = Person.Create(InitializerConstants.FrancUserId, InitializerConstants.FrancFirstName, InitializerConstants.FrancLastName);
            await _context.Persons.AddAsync(franc);

            await _context.SaveChangesAsync();

        }
    }
}
