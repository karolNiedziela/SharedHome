using SharedHome.Domain.Bills.Entities;

namespace SharedHome.Domain.Bills.Services
{
    public interface IBillService
    {
        Task<Bill> GetAsync(int id, string personId);
    }
}
