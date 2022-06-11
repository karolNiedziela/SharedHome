using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Invitations.Commands.Handlers
{
    public class AcceptInvitationHandler : ICommandHandler<AcceptInvitation, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupRepository _houseGroupRepository;

        public AcceptInvitationHandler(IInvitationRepository invitationRepository, 
            IHouseGroupRepository houseGroupRepository)
        {
            _invitationRepository = invitationRepository;
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(AcceptInvitation request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId!);

            invitation.Accept();

            await _invitationRepository.UpdateAsync(invitation);

            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(invitation.HouseGroupId, request.PersonId!);
            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, request.PersonId!));

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
