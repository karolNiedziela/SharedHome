using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Tests.Shared.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedHome.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(WriteSharedHomeDbContext writeContext)
        {
            writeContext.Persons.AddRange(GetSeedingPersons());
            writeContext.SaveChanges();
        }

        public static List<Person> GetSeedingPersons()
        {
            return new List<Person>
            {
                Person.Create(ShoppingListProvider.PersonId, "Shopping", "List"),
            };
        }
    }
}
