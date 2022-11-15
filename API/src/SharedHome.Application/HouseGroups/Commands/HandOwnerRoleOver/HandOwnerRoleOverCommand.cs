﻿using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.HouseGroups.Commands.HandOwnerRoleOver
{
    public class HandOwnerRoleOverCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid HouseGroupId { get; set; }

        public Guid NewOwnerPersonId { get; set; } = default!;
    }
}
