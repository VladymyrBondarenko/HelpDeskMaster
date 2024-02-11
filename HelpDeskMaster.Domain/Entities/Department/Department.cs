using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.Department
{
    public class Department : Entity
    {
        public Department(Guid id, DateTimeOffset createdAt, 
            string title, 
            Guid? parentDepartmentId, 
            string? address) : base(id, createdAt)
        {
            Title = Guard.Against.NullOrWhiteSpace(title);
            ParentDepartmentId = parentDepartmentId;
            Address = address;
        }

        public string Title { get; private set; }

        public Guid? ParentDepartmentId { get; private set; }

        public string? Address { get; private set; }
    }
}