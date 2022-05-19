using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Invitations.Commands.Handlers
{
    public class SendInvitationHandler : ICommandHandler<SendInvitation, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IInvitationReadService _invitationService;

        public SendInvitationHandler(IInvitationRepository invitationRepository, IHouseGroupReadService houseGroupService, IInvitationReadService invitationService)
        {
            _invitationRepository = invitationRepository;
            _houseGroupService = houseGroupService;
            _invitationService = invitationService;
        }

        public async Task<Unit> Handle(SendInvitation request, CancellationToken cancellationToken)
        {    
            if (!await _houseGroupService.IsPersonInHouseGroup(request.PersonId!, request.HouseGroupId)) 
            {
                throw new PersonIsNotInHouseGroupException(request.PersonId!, request.HouseGroupId);
            }

            if (await _invitationService.IsAnyInvitationFromHouseGroupToPerson(request.HouseGroupId!, request.RequestedToPersonId))
            {
                throw new InvitationAlreadySentException(request.HouseGroupId, request.RequestedToPersonId!);
            }

            var invitation = Invitation.Create(request.HouseGroupId, request.PersonId!, request.RequestedToPersonId);

            await _invitationRepository.AddAsync(invitation);

            return Unit.Value;
        }
    }
}
