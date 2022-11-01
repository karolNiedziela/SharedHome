using SharedHome.Application.Persons.Exceptions;
using SharedHome.Domain.Persons;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Domain.Persons.ValueObjects;

namespace SharedHome.Application.Persons.Extensions
{
    public static class PersonRepositoryExtensions
    {
        public static async Task<Person> GetByEmailOrThrowAsync(this IPersonRepository personRepository, Email email)
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
