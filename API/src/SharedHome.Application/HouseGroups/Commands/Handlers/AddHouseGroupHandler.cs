using MediatR;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Application.HouseGroups.DTO;
using AutoMapper;

namespace SharedHome.Application.HouseGroups.Commands.Handlers
{
    public class AddHouseGroupHandler : ICommandHandler<AddHouseGroup, Response<HouseGroupDto>>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IMapper _mapper;

        public AddHouseGroupHandler(IHouseGroupRepository houseGroupRepository, IHouseGroupReadService houseGroupService, IMapper mapper)
        {
            _houseGroupRepository = houseGroupRepository;
            _houseGroupService = houseGroupService;
            _mapper = mapper;
        }

        public async Task<Response<HouseGroupDto>> Handle(AddHouseGroup request, CancellationToken cancellationToken)
        {
            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                throw new PersonIsAlreadyInHouseGroupException(request.PersonId!);
            }
            var houseGroup = HouseGroup.Create();

            await _houseGroupRepository.AddAsync(houseGroup);

            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, request.PersonId!, true));

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return new Response<HouseGroupDto>(_mapper.Map<HouseGroupDto>(houseGroup));
        }
    }
}
