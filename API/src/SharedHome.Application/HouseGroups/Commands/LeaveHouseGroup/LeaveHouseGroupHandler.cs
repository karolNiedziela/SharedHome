using MediatR;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands.LeaveHouseGroup
{
    public class LeaveHouseGroupHandler : ICommandHandler<LeaveHouseGroupCommand, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public LeaveHouseGroupHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(LeaveHouseGroupCommand request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId!);

            houseGroup.Leave(request.PersonId!);

            if (!houseGroup.Members.Any())
            {
                await _houseGroupRepository.DeleteAsync(houseGroup);
                return Unit.Value;
            }

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
