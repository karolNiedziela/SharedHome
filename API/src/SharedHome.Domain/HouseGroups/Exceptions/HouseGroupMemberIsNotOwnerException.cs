﻿using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupMemberIsNotOwnerException : SharedHomeException
    {
        public override string ErrorCode => "HouseGroupMemberIsNotOwner";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        [Order]
        public Guid PersonId { get; }

        public HouseGroupMemberIsNotOwnerException(Guid personId) : base($"House group member with person id '{personId}' is not owner.")
        {
            PersonId = personId;
        }

    }
}
