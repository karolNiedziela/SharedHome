using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class HouseGroupMemberNotFoundException : SharedHomeException
    {
        public string PersonId { get; }

        public HouseGroupMemberNotFoundException(string personId) : base($"House group member with person id: '{personId}' was not found.")
        {
            PersonId = personId;
        }

    }
}
