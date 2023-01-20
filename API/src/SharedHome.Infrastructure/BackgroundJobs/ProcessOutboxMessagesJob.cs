using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using SharedHome.Domain.Primivites;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly WriteSharedHomeDbContext _context;
        private readonly IPublisher _publisher;
        private readonly ILogger<ProcessOutboxMessagesJob> _logger;

        public ProcessOutboxMessagesJob(WriteSharedHomeDbContext context, IPublisher publisher, ILogger<ProcessOutboxMessagesJob> logger)
        {
            _context = context;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _context.OutboxMessages
                .Where(outboxMessage => outboxMessage.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            if (!messages.Any())
            {
                return;
            }

            foreach (var outboxMessage in messages)
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                if (domainEvent is null)
                {
                    _logger.LogWarning("Domain event content is empty. Domain event type: {domainEventType}", outboxMessage.Type);
                    continue;
                }

                try
                {
                    await _publisher.Publish(domainEvent, context.CancellationToken);
                }
                catch (Exception ex)
                {
                    outboxMessage.Error = ex.Message;
                    continue;
                }


                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
