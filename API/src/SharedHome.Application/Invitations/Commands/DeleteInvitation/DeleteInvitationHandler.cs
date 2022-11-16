using MediatR;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Domain.Invitations.Repositories;


namespace SharedHome.Application.Invitations.Commands.DeleteInvitation
{
    public class DeleteInvitationHandler : IRequestHandler<DeleteInvitationCommand, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;

        public DeleteInvitationHandler(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<Unit> Handle(DeleteInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId);

            await _invitationRepository.DeleteAsync(invitation);

            return Unit.Value;
        }
    }
}
