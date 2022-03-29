﻿using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupMemberIsNotOwnerException : SharedHomeException
    {
        public string PersonId { get; }

        public HouseGroupMemberIsNotOwnerException(string personId) : base($"House group member with person id '{personId}' is not owner.")
        {
            PersonId = personId;
        }

    }
}
