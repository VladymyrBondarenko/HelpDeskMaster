using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.WorkRequests
{
    public class WorkRequestPost : Entity
    {
        public WorkRequestPost(Guid id, DateTimeOffset createdAt, 
            Guid userId, 
            Guid workRequestId, 
            string content) : base(id, createdAt)
        {
            UserId = Guard.Against.Default(userId);
            WorkRequestId = Guard.Against.Default(workRequestId);
            Content = Guard.Against.NullOrWhiteSpace(content);
        }

        public Guid UserId { get; private set; }

        public Guid WorkRequestId { get; private set; }

        public string Content { get; private set; }
    }
}
