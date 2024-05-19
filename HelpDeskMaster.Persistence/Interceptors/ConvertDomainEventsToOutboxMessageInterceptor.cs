using HelpDeskMaster.Domain.Abstractions;
using HelpDeskMaster.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace HelpDeskMaster.Persistence.Interceptors
{
    internal class ConvertDomainEventsToOutboxMessageInterceptor
        : SaveChangesInterceptor
    {
        public async override ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, 
            int result, 
            CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if(dbContext == null)
            {
                return await base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            var outboxMessages = dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .SelectMany(aggregate =>
                {
                    var domainEvents = aggregate.GetDomainEvents();

                    aggregate.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = DateTimeOffset.UtcNow,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(domainEvent,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                })
                .ToList();

            if(outboxMessages.Count > 0)
            {
                await dbContext.Set<OutboxMessage>()
                    .AddRangeAsync(outboxMessages, cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
