using MediatR;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Invitations.Commands.Handlers
{
    public class DeleteInvitationHandler : ICommandHandler<DeleteInvitation, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;

        public DeleteInvitationHandler(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<Unit> Handle(DeleteInvitation request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId!);

            await _invitationRepository.DeleteAsync(invitation);

            return Unit.Value;
        }
    }
}
