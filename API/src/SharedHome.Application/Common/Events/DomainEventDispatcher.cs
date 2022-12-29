using MediatR;
using Microsoft.Extensions.Logging;
using SharedHome.Domain.Common.Events;

namespace SharedHome.Application.Common.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DomainEventDispatcher> _logger;        

        public DomainEventDispatcher(IMediator mediator, ILogger<DomainEventDispatcher> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task DispatchAsync(IDomainEvent @event)
        {
            var domainEventNotification = CreateDomainEventNotification(@event);

            _logger.LogInformation("Dispatching Domain Event. EventType: {EventType}.", @event.GetType());

            await _mediator.Publish(domainEventNotification);
        }

        private static INotification CreateDomainEventNotification(IDomainEvent @event)
        {
            var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(@event.GetType());

            return (INotification)Activator.CreateInstance(genericDispatcherType, @event)!;
        }
    }
}
