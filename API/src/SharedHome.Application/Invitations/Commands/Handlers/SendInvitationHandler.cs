using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Invitations.Commands.Handlers
{
    public class SendInvitationHandler : ICommandHandler<SendInvitation, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupRepository _houseGroupRepostiory;

        public SendInvitationHandler(IInvitationRepository invitationRepository, IHouseGroupRepository houseGroupRepostiory)
        {
            _invitationRepository = invitationRepository;
            _houseGroupRepostiory = houseGroupRepostiory;
        }

        public async Task<Unit> Handle(SendInvitation request, CancellationToken cancellationToken)
        {    
            if (!await _houseGroupRepostiory.IsPersonInHouseGroup(request.PersonId!, request.HouseGroupId)) 
            {
                throw new PersonIsNotInHouseGroup(request.PersonId!, request.HouseGroupId);
            }

            if (await _invitationRepository.IsAnyInvitationFromHouseGroupToPerson(request.HouseGroupId, request.PersonId!))
            {
                throw new InvitationAlreadySentException(request.HouseGroupId, request.PersonId!);
            }

            var invitation = Invitation.Create(request.HouseGroupId, request.RequestedToPersonId);

            await _invitationRepository.AddAsync(invitation);

            return Unit.Value;
        }
    }
}
