// # ==============================================================================
// # Solution: BiteRight
// # File: DomainEvent.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

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