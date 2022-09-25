namespace SharedHome.Application.ReadServices
{
    public interface IHouseGroupReadService
    {
        Task<bool> IsPersonInHouseGroup(string personId);

        Task<bool> IsPersonInHouseGroup(string personId, int houseGroupId);

        Task<IEnumerable<string>> GetMemberPersonIds(string personId);

        Task<IEnumerable<string>> GetMemberPersonIdsExcludingCreator(string personId);
    }
}
