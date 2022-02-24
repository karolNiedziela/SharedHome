using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
    }
}
