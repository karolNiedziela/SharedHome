using MediatR;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Domain.Invitations.Repositories;


namespace SharedHome.Application.Invitations.Commands.RejectInvitation
{
    public class RejectInvitationHandler : IRequestHandler<RejectInvitationCommand, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;

        public RejectInvitationHandler(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<Unit> Handle(RejectInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId);

            invitation.Reject();

            await _invitationRepository.UpdateAsync(invitation);

            return Unit.Value;
        }
    }
}
