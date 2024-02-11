using System.Collections.Generic;

namespace BiteRight.Domain.Common;

public class AggregateRoot<TId> : Entity<TId>, IDomainEventHolder
    where TId : GuidId
{
    private readonly List<DomainEvent> _domainEvents = [];

    protected AggregateRoot()
    {
    }

    public AggregateRoot(
        TId id
    )
        : base(id)
    {
    }

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(
        DomainEvent domainEvent
    )
    {
        _domainEvents.Add(domainEvent);
    }
}