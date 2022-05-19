using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.ShoppingLists.Aggregates;

namespace SharedHome.Application.Services
{
    public interface IHouseGroupReadService
    {
        Task<bool> IsPersonInHouseGroup(string personId);

        Task<bool> IsPersonInHouseGroup(string personId, int houseGroupId);

        Task<IEnumerable<string>> GetMemberPersonIds(string personId);
    }
}
