using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.WorkRequests
{
    public class WorkDirection : Entity
    {
        public WorkDirection(Guid id, DateTimeOffset createdAt,
            string title) : base(id, createdAt)
        {
            Title = Guard.Against.NullOrWhiteSpace(title);
        }

        public string Title { get; private set; }
    }
}