using MediatR;
using SharedHome.Shared.Abstractions.Domain;

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
