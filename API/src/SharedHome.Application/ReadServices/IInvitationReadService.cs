using SharedHome.Domain.Invitations;

namespace SharedHome.Application.ReadServices
{
    public interface IInvitationReadService
    {
        Task<bool> IsAnyInvitationFromHouseGroupToPersonAsync(Guid houseGroupId, Guid requestedToPersonId);
    }
}
