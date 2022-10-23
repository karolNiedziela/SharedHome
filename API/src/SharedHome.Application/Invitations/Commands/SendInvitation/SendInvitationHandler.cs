using MapsterMapper;
using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Persons.Extensions;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Commands.SendInvitation
{
    public class SendInvitationHandler : ICommandHandler<SendInvitationCommand, Response<InvitationDto>>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IInvitationReadService _invitationService;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public SendInvitationHandler(IInvitationRepository invitationRepository, IHouseGroupReadService houseGroupService, IInvitationReadService invitationService, IMapper mapper, IPersonRepository personRepository)
        {
            _invitationRepository = invitationRepository;
            _houseGroupService = houseGroupService;
            _invitationService = invitationService;
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<Response<InvitationDto>> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByEmailOrThrowAsync(request.RequestedToPersonEmail);

            if (!await _houseGroupService.IsPersonInHouseGroup(request.PersonId, request.HouseGroupId)) 
            {
                throw new PersonIsNotInHouseGroupException(request.PersonId, request.HouseGroupId);
            }

            if (await _invitationService.IsAnyInvitationFromHouseGroupToPerson(request.HouseGroupId, person.Id))
            {
                throw new InvitationAlreadySentException(request.HouseGroupId, person.Id);
            }

            var invitation = Invitation.Create(request.InvitationId, request.HouseGroupId, request.PersonId!, person.Id);

            await _invitationRepository.AddAsync(invitation);

            return new Response<InvitationDto>(_mapper.Map<InvitationDto>(invitation));
        }
    }
}
