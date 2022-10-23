using SharedHome.Domain.Bills.Entities;

namespace SharedHome.Domain.Bills.Services
{
    public interface IBillService
    {
        Task<Bill> GetAsync(Guid id, Guid personId);
    }
}
