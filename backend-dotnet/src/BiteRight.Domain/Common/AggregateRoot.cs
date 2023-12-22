namespace BiteRight.Domain.Common;

public class AggregateRoot<TId> : Entity<TId>, IDomainEventHolder
    where TId : GuidId
{
    protected AggregateRoot()
    {
    }
    public AggregateRoot(
        TId id
    )
        : base(id)
    {
    }

    private readonly List<DomainEvent> _domainEvents = [];
    
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(
        DomainEvent domainEvent
    )
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}