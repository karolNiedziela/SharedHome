using MapsterMapper;
using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Invitations.Commands.Handlers
{
    public class SendInvitationHandler : ICommandHandler<SendInvitation, Response<InvitationDto>>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IInvitationReadService _invitationService;
        private readonly IMapper _mapper;

        public SendInvitationHandler(IInvitationRepository invitationRepository, IHouseGroupReadService houseGroupService, IInvitationReadService invitationService, IMapper mapper)
        {
            _invitationRepository = invitationRepository;
            _houseGroupService = houseGroupService;
            _invitationService = invitationService;
            _mapper = mapper;
        }

        public async Task<Response<InvitationDto>> Handle(SendInvitation request, CancellationToken cancellationToken)
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

            return new Response<InvitationDto>(_mapper.Map<InvitationDto>(invitation));
        }
    }
}
