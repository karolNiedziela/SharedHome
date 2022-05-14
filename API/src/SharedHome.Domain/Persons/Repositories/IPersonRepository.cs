using SharedHome.Domain.Persons.Aggregates;

namespace SharedHome.Domain.Persons.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetAsync(string id);

        Task AddAsync(Person person);
    }
}
