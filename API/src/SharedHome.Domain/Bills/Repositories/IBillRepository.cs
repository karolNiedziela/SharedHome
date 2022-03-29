using SharedHome.Domain.Bills.Entities;

namespace SharedHome.Domain.Bills.Repositories
{
    public interface IBillRepository
    {
        Task<Bill?> GetAsync(int id, string personId);

        Task AddAsync(Bill bill);

        Task DeleteAsync(Bill bill);

        Task UpdateAsync(Bill bill);
    }
}
