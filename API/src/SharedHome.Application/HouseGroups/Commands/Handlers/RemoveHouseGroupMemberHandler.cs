﻿using MediatR;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands.Handlers
{
    public class RemoveHouseGroupMemberHandler : ICommandHandler<RemoveHouseGroupMember, Unit>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public RemoveHouseGroupMemberHandler(IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Unit> Handle(RemoveHouseGroupMember request, CancellationToken cancellationToken)
        {
            var houseGroup = await _houseGroupRepository.GetOrThrowAsync(request.HouseGroupId, request.PersonId!);

            houseGroup.RemoveMember(request.PersonId!, request.PersonToRemoveId);

            await _houseGroupRepository.UpdateAsync(houseGroup);

            return Unit.Value;
        }
    }
}
