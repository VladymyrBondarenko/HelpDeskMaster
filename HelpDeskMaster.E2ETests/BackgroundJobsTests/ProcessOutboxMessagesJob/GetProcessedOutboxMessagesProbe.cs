using HelpDeskMaster.E2ETests.Probing;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.E2ETests.BackgroundJobsTests.ProcessOutboxMessagesJob
{
    public class GetProcessedOutboxMessagesProbe : IProbe
    {
        private readonly DbContext _dbContext;
        private readonly IEnumerable<OutboxMessage> _pulledOutboxMessages;
        private Dictionary<Guid, OutboxMessage>? _outboxMessageInDb;
        private string _failureDescription = string.Empty;

        public GetProcessedOutboxMessagesProbe(
            ApplicationDbContext dbContext,
            OutboxMessage pulledOutboxMessage) : this(dbContext, new[] { pulledOutboxMessage })
        {
        }

        public GetProcessedOutboxMessagesProbe(
            ApplicationDbContext dbContext,
            IEnumerable<OutboxMessage> pulledOutboxMessages)
        {
            _dbContext = dbContext;
            _pulledOutboxMessages = pulledOutboxMessages;
        }

        public string DescribeFailureTo() => _failureDescription;

        public bool IsSatisfied()
        {
            if (_outboxMessageInDb == null)
            {
                _failureDescription = "Outbox messages were not loaded from database";
                return false;
            }

            foreach (var outboxMessage in _pulledOutboxMessages)
            {
                if (!_outboxMessageInDb.TryGetValue(outboxMessage.Id, out var messageInDb))
                {
                    _failureDescription = $"Outbox message with id {outboxMessage.Id} wasn't found in database";
                    return false;
                }

                if (messageInDb?.ProcessedOnUtc == null)
                {
                    _failureDescription = $"Outbox message with id {outboxMessage.Id} wasn't processed";
                    return false;
                }
            }

            return true;
        }

        public async Task SampleAsync()
        {
            var query = """
                SELECT 
                    "Id", 
                    "Type", 
                    "Content", 
                    "OccuredOnUtc", 
                    "ProcessedOnUtc", 
                    "Error"
                FROM "OutboxMessages"
                """;

            _outboxMessageInDb = await _dbContext.Database
                .SqlQueryRaw<OutboxMessage>(query)
                .ToDictionaryAsync(x => x.Id);
        }
    }
}