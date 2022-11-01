using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Domain.Invitations;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Application.Invitations.Extensions
{
    public static class InvitationRepositoryExtensions
    {
        public static async Task<Invitation> GetOrThrowAsync(this IInvitationRepository repository, 
            HouseGroupId houseGroupId, PersonId personId)
        {
            var invitation = await repository.GetAsync(houseGroupId, personId);
            if (invitation is null)
            {
                throw new InvitationNotFoundException(houseGroupId);
            }

            return invitation;
        }
    }
}
