using MediatR;
using SharedHome.Domain.Common.Events;

namespace SharedHome.Application.Common.Events
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; } = default!;

        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
