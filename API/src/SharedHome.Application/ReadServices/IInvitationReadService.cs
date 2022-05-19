namespace SharedHome.Application.ReadServices
{
    public interface IInvitationReadService
    {
        Task<bool> IsAnyInvitationFromHouseGroupToPerson(int houseGroupId, string requestedToPersonId);
    }
}
