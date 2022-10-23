using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;

namespace SharedHome.Application.Invitations.Extensions
{
    public static class InvitationRepositoryExtensions
    {
        public static async Task<Invitation> GetOrThrowAsync(this IInvitationRepository repository, 
            Guid houseGroupId, Guid personId)
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
