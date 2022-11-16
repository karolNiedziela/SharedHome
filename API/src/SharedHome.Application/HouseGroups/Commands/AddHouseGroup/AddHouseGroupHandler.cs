﻿using MapsterMapper;
using MediatR;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;

using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.HouseGroups.Commands.AddHouseGroup
{
    public class AddHouseGroupHandler : IRequestHandler<AddHouseGroupCommand, Response<HouseGroupDto>>
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

        public async Task<Response<HouseGroupDto>> Handle(AddHouseGroupCommand request, CancellationToken cancellationToken)
        {
            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId))
            {
                throw new PersonIsAlreadyInHouseGroupException(request.PersonId);
            }
            var houseGroup = HouseGroup.Create(request.HouseGroupId, request.Name);

            await _houseGroupRepository.AddAsync(houseGroup);

            houseGroup.AddMember(new HouseGroupMember(houseGroup.Id, request.PersonId, true));

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return new Response<HouseGroupDto>(_mapper.Map<HouseGroupDto>(houseGroup));
        }
    }
}
