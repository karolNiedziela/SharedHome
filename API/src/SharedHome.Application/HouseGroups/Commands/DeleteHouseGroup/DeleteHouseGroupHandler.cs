using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup
{
    public class DeleteHouseGroupHandler : ICommandHandler<DeleteHouseGroupCommand, Response<Unit>>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public DeleteHouseGroupHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteHouseGroupCommand request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId);

            if (!houseGroup.IsOwner(request.PersonId))
            {
                throw new HouseGroupMemberIsNotOwnerException(request.PersonId);
            }

            await _houseGroupRepository.DeleteAsync(houseGroup);

            return new Response<Unit>(Unit.Value);
        }
    }
}
