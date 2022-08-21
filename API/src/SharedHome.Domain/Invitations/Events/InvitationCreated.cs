using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Events
{
    public record InvitationCreated(int HouseGroupId, Guid PersonId) : IDomainEvent;
}
