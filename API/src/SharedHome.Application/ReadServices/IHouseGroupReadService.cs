namespace SharedHome.Application.ReadServices
{
    public interface IHouseGroupReadService
    {
        Task<bool> IsPersonInHouseGroup(Guid personId);

        Task<bool> IsPersonInHouseGroup(Guid personId, Guid houseGroupId);

        Task<IEnumerable<Guid>> GetMemberPersonIds(Guid personId);

        Task<IEnumerable<Guid>> GetMemberPersonIdsExcludingCreator(Guid personId);
    }
}
