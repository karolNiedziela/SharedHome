using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Application.Invitations.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.HouseGroups.Exceptions;

namespace SharedHome.Application.Invitations.Commands.AcceptInvitation
{
    public class AcceptInvitationHandler : ICommandHandler<AcceptInvitationCommand, Unit>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;

        public AcceptInvitationHandler(IInvitationRepository invitationRepository,
            IHouseGroupRepository houseGroupRepository, IHouseGroupReadService houseGroupReadService)
        {
            _invitationRepository = invitationRepository;
            _houseGroupRepository = houseGroupRepository;
            _houseGroupReadService = houseGroupReadService;
        }

        public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            if (await _houseGroupReadService.IsPersonInHouseGroup(request.PersonId))
            {
                throw new PersonIsAlreadyInHouseGroupException(request.PersonId);
            }

            var invitation = await _invitationRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId);

            invitation.Accept();

            await _invitationRepository.UpdateAsync(invitation);

            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(invitation.HouseGroupId, request.PersonId);
            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, request.PersonId));

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
