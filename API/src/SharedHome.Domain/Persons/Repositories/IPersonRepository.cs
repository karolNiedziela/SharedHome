using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.Persons.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetAsync(PersonId id);

        Task<Person?> GetByEmailAsync(Email email);

        Task AddAsync(Person person);
    }
}
