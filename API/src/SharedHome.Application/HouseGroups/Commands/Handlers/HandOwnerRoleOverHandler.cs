using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands.Handlers
{
    public class HandOwnerRoleOverHandler : ICommandHandler<HandOwnerRoleOver, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public HandOwnerRoleOverHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(HandOwnerRoleOver request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId!);

            houseGroup.HandOwnerRoleOver(request.PersonId!, request.NewOwnerPersonId);

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
