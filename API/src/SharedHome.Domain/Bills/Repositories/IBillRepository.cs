using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Domain.Bills.Repositories
{
    public interface IBillRepository
    {
        Task<Bill?> GetAsync(BillId id, PersonId personId);

        Task<Bill?> GetAsync(BillId id, IEnumerable<PersonId> personIds);

        Task AddAsync(Bill bill);

        Task DeleteAsync(Bill bill);

        Task UpdateAsync(Bill bill);
    }
}
