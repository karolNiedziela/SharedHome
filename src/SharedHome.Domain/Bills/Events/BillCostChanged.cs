using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.Events
{
    public record BillCostChanged(int BillId, ServiceProviderName ServiceProviderName, Money Cost) : IEvent;
}
