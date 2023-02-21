using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;
using Persistence.Outbox;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        _dbContext=dbContext;
        _publisher=publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext.Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            // TODO: Add Configuration for outbox message Jobs Take
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(outboxMessage.Content);

            if (domainEvent is null)
            {
                // TODO: Add Logging
                continue;
            }

            await _publisher.Publish(domainEvent, context.CancellationToken);

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}
