using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.HouseGroups.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands.Handlers
{
    public class AddHouseGroupHandler : ICommandHandler<AddHouseGroup, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public AddHouseGroupHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(AddHouseGroup request, CancellationToken cancellationToken)
        {
            if (await _houseGroupRepository.IsPersonInHouseGroup(request.PersonId!))
            {
                throw new PersonIsAlreadyInHouseGroupException(request.PersonId!);
            }

            var houseGroup = HouseGroup.Create(new HouseGroupMember(request.PersonId!, request.FirstName!, 
                request.LastName!, request.Email!, true));

            await _houseGroupRepository.AddAsync(houseGroup);

            return Unit.Value;
        }
    }
}
