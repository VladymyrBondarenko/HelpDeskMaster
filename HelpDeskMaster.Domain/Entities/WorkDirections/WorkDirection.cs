using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.WorkDirections
{
    public class WorkDirection : Entity
    {
        public WorkDirection(
            Guid id, 
            string title, 
            DateTimeOffset createdAt) : base(id, createdAt)
        {
            Title = Guard.Against.NullOrWhiteSpace(title);
        }

        public string Title { get; private set; }
    }
}