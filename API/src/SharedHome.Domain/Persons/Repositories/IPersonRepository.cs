using SharedHome.Domain.Persons.Aggregates;

namespace SharedHome.Domain.Persons.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetAsync(string id);

        Task<Person?> GetByEmailAsync(string email);

        Task AddAsync(Person person);
    }
}
