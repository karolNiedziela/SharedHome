using MapsterMapper;
using MediatR;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;

using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.HouseGroups.Commands.AddHouseGroup
{
    public class AddHouseGroupHandler : IRequestHandler<AddHouseGroupCommand, ApiResponse<HouseGroupDto>>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IMapper _mapper;

        public AddHouseGroupHandler(IHouseGroupRepository houseGroupRepository, IMapper mapper)
        {
            _houseGroupRepository = houseGroupRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HouseGroupDto>> Handle(AddHouseGroupCommand request, CancellationToken cancellationToken)
        {
            if (await _houseGroupRepository.IsPersonInHouseGroupAsync(request.PersonId))
            {
                throw new PersonIsAlreadyInHouseGroupException(request.PersonId);
            }
            var houseGroup = HouseGroup.Create(Guid.NewGuid(), request.Name);

            await _houseGroupRepository.AddAsync(houseGroup);

            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, request.PersonId, true));

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return new ApiResponse<HouseGroupDto>(_mapper.Map<HouseGroupDto>(houseGroup));
        }
    }
}
