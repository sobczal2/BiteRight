namespace BiteRight.Domain.Common;

public interface IDomainEventHolder
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}