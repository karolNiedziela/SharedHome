using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup
{
    public class DeleteHouseGroupHandler : ICommandHandler<DeleteHouseGroupCommand, Response<Unit>>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IInvitationRepository _invitationRepository;

        public DeleteHouseGroupHandler(IHouseGroupRepository houseGroupRepository, IInvitationRepository invitationRepository)
        {
            _houseGroupRepository = houseGroupRepository;
            _invitationRepository = invitationRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteHouseGroupCommand request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId!);

            await _houseGroupRepository.DeleteAsync(houseGroup);

            var invitations = await _invitationRepository.GetAllAsync(request.HouseGroupId);

            await _invitationRepository.DeleteAsync(invitations);

            return new Response<Unit>(Unit.Value);
        }
    }
}
