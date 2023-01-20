using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Events;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Persons.Extensions;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Invitations;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Persons.Repositories;

namespace SharedHome.Application.Invitations.Commands.SendInvitation
{
    public class SendInvitationHandler : IRequestHandler<SendInvitationCommand, Guid>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IInvitationReadService _invitationService;
        private readonly IPersonRepository _personRepository;

        public SendInvitationHandler(
            IInvitationRepository invitationRepository,
            IHouseGroupReadService houseGroupService,
            IInvitationReadService invitationService,
            IPersonRepository personRepository)
        {
            _invitationRepository = invitationRepository;
            _houseGroupService = houseGroupService;
            _invitationService = invitationService;
            _personRepository = personRepository;
        }

        public async Task<Guid> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByEmailOrThrowAsync(request.RequestedToPersonEmail);

            if (!await _houseGroupService.IsPersonInHouseGroupAsync(request.PersonId, request.HouseGroupId)) 
            {
                throw new PersonIsNotInHouseGroupException(request.PersonId, request.HouseGroupId);
            }

            if (await _invitationService.IsAnyInvitationFromHouseGroupToPersonAsync(request.HouseGroupId, person.Id))
            {
                throw new InvitationAlreadySentException(request.HouseGroupId, person.Id);
            }

            var pendingInvitation = Invitation.CreatePending(Guid.NewGuid(), request.HouseGroupId, request.PersonId!, person.Id);

            await _invitationRepository.AddAsync(pendingInvitation);

            var sentInvitation = Invitation.CreateSent(Guid.NewGuid(), request.HouseGroupId, person.Id, request.PersonId);

            await _invitationRepository.AddAsync(sentInvitation);            

            return sentInvitation.Id;
        }
    }
}
