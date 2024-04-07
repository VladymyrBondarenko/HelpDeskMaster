using Ardalis.GuardClauses;

namespace HelpDeskMaster.Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        protected Entity(Guid id, DateTimeOffset createdAt)
        {
            Id = id;
            CreatedAt = Guard.Against.Default(createdAt);
        }

        public Guid Id { get; init; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset? UpdatedAt { get; protected set; }
    }
}