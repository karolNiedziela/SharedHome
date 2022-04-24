namespace SharedHome.Application.Services
{
    public interface IHouseGroupService
    {
        Task<bool> IsPersonInHouseGroup(string personId);

        Task<bool> IsPersonInHouseGroup(string personId, int houseGroupId);
    }
}
