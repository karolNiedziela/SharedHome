using SharedHome.Domain.Persons.Repositories;
using SharedHome.Domain.Persons.Aggregates;
using SharedHome.Application.Persons.Exceptions;

namespace SharedHome.Application.Persons.Extensions
{
    public static class PersonRepositoryExtensions
    {
        public static async Task<Person> GetByEmailOrThrowAsync(this IPersonRepository personRepository, string email)
        {
            var person = await personRepository.GetByEmailAsync(email);
            if (person is null)
            {
                throw new PersonWithEmailNotFoundException(email);
            }

            return person;
        }
    }
}
