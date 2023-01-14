using MediatR;
using Microsoft.Extensions.Logging;
using SharedHome.Domain.Common.Events;
using SharedHome.Shared.Time;

namespace SharedHome.Application.Common.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DomainEventDispatcher> _logger;
        private readonly ITimeProvider _timeProvider;

        public DomainEventDispatcher(IMediator mediator, ILogger<DomainEventDispatcher> logger, ITimeProvider timeProvider)
        {
            _mediator = mediator;
            _logger = logger;
            _timeProvider = timeProvider;
        }

        public async Task DispatchAsync(IDomainEvent @event)
        {
            var domainEventNotification = CreateDomainEventNotification(@event);

            _logger.LogInformation("{DateTimeUtc}: Dispatching Domain Event. EventType: {EventType}.",
                _timeProvider.CurrentDate(),
                @event.GetType());

            await _mediator.Publish(domainEventNotification);
        }

        private static INotification CreateDomainEventNotification(IDomainEvent @event)
        {
            var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(@event.GetType());

            return (INotification)Activator.CreateInstance(genericDispatcherType, @event)!;
        }
    }
}
