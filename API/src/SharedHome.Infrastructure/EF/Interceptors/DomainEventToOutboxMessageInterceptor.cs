using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using SharedHome.Domain.Primivites;
using SharedHome.Infrastructure.EF.Outbox;
using SharedHome.Shared.Time;

namespace SharedHome.Infrastructure.EF.Interceptors
{
    public sealed class DomainEventToOutboxMessageInterceptor : SaveChangesInterceptor
    {
        private readonly ITimeProvider _time;

        public DomainEventToOutboxMessageInterceptor(ITimeProvider time)
        {
            _time = time;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var outboxMessages = dbContext.ChangeTracker
                          .Entries<IAggregateRoot>()
                          .Select(x => x.Entity)
                          .SelectMany(aggregateRoot =>
                          {
                              var domainEvents = aggregateRoot.Events;

                              aggregateRoot.ClearEvents();

                              return domainEvents;
                          })
                          .Select(domainEvent => new OutboxMessage
                          {
                              Id = Guid.NewGuid(),
                              OccuredOn = _time.CurrentDate(),
                              Type = domainEvent.GetType().Name,
                              Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                              {
                                  TypeNameHandling = TypeNameHandling.All,
                              })
                          })
                          .ToArray();

            if (!outboxMessages.Any())
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
