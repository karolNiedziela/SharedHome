﻿using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.HouseGroups.Commands
{
    public class RemoveHousGroupMember : AuthorizeCommand, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }

        public string PersonToRemoveId { get; set; } = default!;
    }
}
