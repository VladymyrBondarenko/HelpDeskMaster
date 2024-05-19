namespace HelpDeskMaster.Domain.Abstractions
{
    public class AggregateRoot : Entity
    {
        protected AggregateRoot() { }

        protected AggregateRoot(Guid id, DateTimeOffset createdAt) : base(id, createdAt) 
        { 
        }

        private readonly List<IDomainEvent> _domainEvents = new ();

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}