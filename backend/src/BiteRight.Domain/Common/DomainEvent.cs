namespace BiteRight.Domain.Common;

public abstract class DomainEvent
{
    public DateTime OccurredOn { get; }
    public Guid CorrelationId { get; }

    protected DomainEvent(
        DateTime occurredOn,
        Guid correlationId
    )
    {
        OccurredOn = occurredOn;
        CorrelationId = correlationId;
    }
}