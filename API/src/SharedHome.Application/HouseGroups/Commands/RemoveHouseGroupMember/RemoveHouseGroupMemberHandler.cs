using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;


namespace SharedHome.Application.HouseGroups.Commands.RemoveHouseGroupMember
{
    public class RemoveHouseGroupMemberHandler : IRequestHandler<RemoveHouseGroupMemberCommand, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public RemoveHouseGroupMemberHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(RemoveHouseGroupMemberCommand request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId);

            houseGroup.RemoveMember(request.PersonId, request.PersonToRemoveId);

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
