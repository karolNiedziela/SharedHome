namespace SharedHome.Application.ReadServices
{
    public interface IHouseGroupReadService
    {
        Task<bool> IsPersonInHouseGroupAsync(Guid personId);

        Task<bool> IsPersonInHouseGroupAsync(Guid personId, Guid houseGroupId);

        Task<IEnumerable<Guid>> GetMemberPersonIdsAsync(Guid personId);

        Task<IEnumerable<Guid>> GetMemberPersonIdsExcludingCreatorAsync(Guid personId);
    }
}
