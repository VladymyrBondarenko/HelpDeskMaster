using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.WorkCategories
{
    public class WorkCategory : Entity
    {
        internal WorkCategory(Guid id, DateTimeOffset createdAt,
            string title) : base(id, createdAt)
        {
            Title = Guard.Against.NullOrWhiteSpace(title);
        }

        public string Title { get; private set; }
    }
}
