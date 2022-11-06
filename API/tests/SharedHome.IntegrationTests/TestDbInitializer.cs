using SharedHome.Domain.Persons;
using SharedHome.Infrastructure.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedHome.IntegrationTests
{
    public static class TestDbInitializer
    {
        public static readonly Guid PersonId = new("6413030d-8545-4db8-a3fd-147c98171180");

        public static async Task Initialize(WriteSharedHomeDbContext writeContext)
        {
            await writeContext.Persons.AddRangeAsync(GetSeedingPersons());
            await writeContext.SaveChangesAsync();            
        }

        public static List<Person> GetSeedingPersons()
        {
            return new List<Person>
            {
                Person.Create(PersonId, "Shopping", "List", "firstPerson@email.com"),
            };
        }
    }
}
