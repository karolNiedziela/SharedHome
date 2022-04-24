using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Services;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Invitations.Commands.Handlers
{
    public class SendInvitationHandler : ICommandHandler<SendInvitation, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupService _houseGroupService;
        private readonly IInvitationService _invitationService;

        public SendInvitationHandler(IInvitationRepository invitationRepository, IHouseGroupService houseGroupService, IInvitationService invitationService)
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

            if (await _invitationService.IsAnyInvitationFromHouseGroupToPerson(request.HouseGroupId, request.PersonId!))
            {
                throw new InvitationAlreadySentException(request.HouseGroupId, request.PersonId!);
            }

            var invitation = Invitation.Create(request.HouseGroupId, request.RequestedToPersonId, 
                request.FirstName!, request.LastName!);

            await _invitationRepository.AddAsync(invitation);

            return Unit.Value;
        }
    }
}
