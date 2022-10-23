using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands.HandOwnerRoleOver
{
    public class HandOwnerRoleOverHandler : ICommandHandler<HandOwnerRoleOverCommand, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public HandOwnerRoleOverHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(HandOwnerRoleOverCommand request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId);

            houseGroup.HandOwnerRoleOver(request.PersonId, request.NewOwnerPersonId);

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
