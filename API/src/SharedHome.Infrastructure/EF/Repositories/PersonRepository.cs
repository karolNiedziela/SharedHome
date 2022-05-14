using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly WriteSharedHomeDbContext _context;

        public PersonRepository(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetAsync(string id)
            => await _context.Persons.SingleOrDefaultAsync(person => person.Id == id);

        public async Task AddAsync(Person person)
        {
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
        }
    }
}
