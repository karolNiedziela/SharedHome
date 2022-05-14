using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Services;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.HouseGroups.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands.Handlers
{
    public class AddHouseGroupHandler : ICommandHandler<AddHouseGroup, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IHouseGroupService _houseGroupService;

        public AddHouseGroupHandler(IHouseGroupRepository houseGroupRepository, IHouseGroupService houseGroupService)
        {
            _houseGroupRepository = houseGroupRepository;
            _houseGroupService = houseGroupService;
        }

        public async Task<Unit> Handle(AddHouseGroup request, CancellationToken cancellationToken)
        {
            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                throw new PersonIsAlreadyInHouseGroupException(request.PersonId!);
            }
            var houseGroup = HouseGroup.Create();

            await _houseGroupRepository.AddAsync(houseGroup);

            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, request.PersonId!, true));

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
