using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Invitations.Repositories;

using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.HouseGroups.Commands.DeleteHouseGroup
{
    public class DeleteHouseGroupHandler : IRequestHandler<DeleteHouseGroupCommand, ApiResponse<Unit>>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public DeleteHouseGroupHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<ApiResponse<Unit>> Handle(DeleteHouseGroupCommand request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId);

            if (!houseGroup.IsOwner(request.PersonId))
            {
                throw new HouseGroupMemberIsNotOwnerException(request.PersonId);
            }

            await _houseGroupRepository.DeleteAsync(houseGroup);

            return new ApiResponse<Unit>(Unit.Value);
        }
    }
}
