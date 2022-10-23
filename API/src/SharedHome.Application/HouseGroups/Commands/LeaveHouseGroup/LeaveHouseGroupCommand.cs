﻿using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.HouseGroups.Commands.LeaveHouseGroup
{
    public class LeaveHouseGroupCommand : AuthorizeRequest, ICommand<Unit>
    {
        public Guid HouseGroupId { get; set; }
    }
}
