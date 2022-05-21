using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands.Handlers
{
    public class LeaveHouseGroupHandler : ICommandHandler<LeaveHouseGroup, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public LeaveHouseGroupHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(LeaveHouseGroup request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId!);

            houseGroup.Leave(request.PersonId!, request.NewOwnerId);

            if (houseGroup.Members.Count() == 0)
            {
                await _houseGroupRepository.DeleteAsync(houseGroup);
                return Unit.Value;
            }

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
