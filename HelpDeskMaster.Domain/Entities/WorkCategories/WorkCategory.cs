using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.WorkCategories
{
    public class WorkCategory : Entity
    {
        public WorkCategory(
            Guid id, 
            string title, 
            DateTimeOffset createdAt) : base(id, createdAt)
        {
            Title = Guard.Against.NullOrWhiteSpace(title);
        }

        public string Title { get; private set; }
    }
}
