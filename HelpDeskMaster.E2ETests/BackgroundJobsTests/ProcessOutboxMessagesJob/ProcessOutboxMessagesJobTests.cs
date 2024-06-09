using HelpDeskMaster.Domain.Abstractions;
using HelpDeskMaster.E2ETests.EndpointsTests;
using HelpDeskMaster.E2ETests.Probing;
using HelpDeskMaster.Infrastracture.BackgroundJobs;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.Persistence.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace HelpDeskMaster.E2ETests.BackgroundJobsTests.ProcessOutboxMessagesJob
{
    [Collection(nameof(ProbingTestsCollectionDefinition))]
    public partial class ProcessOutboxMessagesJobTests : IClassFixture<HdmServerApplicationFactory>
    {
        private int _batchSize => _factory.Services
            .GetService<BackgroundJobsOptions>()!
            .Outbox!
            .BatchSize;

        private readonly HdmServerApplicationFactory _factory;

        public ProcessOutboxMessagesJobTests(HdmServerApplicationFactory factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        public async Task ShouldProcessOutboxMessages(int count)
        {
            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var date = new DateTimeOffset(
                2024, 5, 10,
                20, 33, 5,
                new TimeSpan());

            var outboxMessages = new List<OutboxMessage>();

            foreach (var domainEvent in mockDomainEvents(count))
            {
                outboxMessages.Add(new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = date,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(domainEvent,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                });
            }

            await db.Set<OutboxMessage>().AddRangeAsync(outboxMessages);
            await db.SaveChangesAsync();

            // calcualte the number of batches for job to process
            // and give each batch 5 seconds tops to process the batch
            var timeout = (int)Math.Ceiling(count / (_batchSize * 1m)) * 5_000;

            await PollerFacade.AssertEventually(
                new GetProcessedOutboxMessagesProbe(db, outboxMessages),
                timeout);
        }

        private IEnumerable<IDomainEvent> mockDomainEvents(int count)
        {
            var counter = 1;

            while (counter < count)
            {
                yield return new ProcessOutboxMessageTestDomainEvent(
                    Guid.NewGuid().ToString());

                counter++;
            }
        }
    }
}