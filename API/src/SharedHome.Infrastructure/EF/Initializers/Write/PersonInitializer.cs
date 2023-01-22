using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Persons;
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

            var charles = Person.Create(InitializerConstants.CharlesUserId, InitializerConstants.CharlesFirstName, InitializerConstants.CharlesLastName, InitializerConstants.CharlesEmail);
            await _context.Persons.AddAsync(charles);
            charles.ClearEvents();

            var franc = Person.Create(InitializerConstants.FrancUserId, InitializerConstants.FrancFirstName, InitializerConstants.FrancLastName, InitializerConstants.FrancEmail);
            await _context.Persons.AddAsync(franc);
            franc.ClearEvents();

            await _context.SaveChangesAsync();
        }
    }
}
