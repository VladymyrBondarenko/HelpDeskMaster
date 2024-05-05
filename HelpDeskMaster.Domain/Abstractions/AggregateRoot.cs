namespace HelpDeskMaster.Domain.Abstractions
{
    public class AggregateRoot : Entity
    {
        protected AggregateRoot() { }

        protected AggregateRoot(Guid id, DateTimeOffset createdAt) : base(id, createdAt) 
        { 
        }
    }
}