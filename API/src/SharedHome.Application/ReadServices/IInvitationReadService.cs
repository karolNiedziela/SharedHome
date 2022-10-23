namespace SharedHome.Application.ReadServices
{
    public interface IInvitationReadService
    {
        Task<bool> IsAnyInvitationFromHouseGroupToPerson(Guid houseGroupId, Guid requestedToPersonId);
    }
}
