namespace SharedHome.Application.Services
{
    public interface IInvitationService
    {
        Task<bool> IsAnyInvitationFromHouseGroupToPerson(int houseGroupId, string personId);
    }
}
