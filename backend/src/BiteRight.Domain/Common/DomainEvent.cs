using System;

namespace BiteRight.Domain.Common;

public abstract class DomainEvent
{
    protected DomainEvent(
        DateTime occurredOn,
        Guid correlationId
    )
    {
        OccurredOn = occurredOn;
        CorrelationId = correlationId;
    }

    public DateTime OccurredOn { get; }
    public Guid CorrelationId { get; }
}