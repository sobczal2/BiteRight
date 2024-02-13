// # ==============================================================================
// # Solution: BiteRight
// # File: MediatorDomainEventNotification.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common;
using MediatR;

#endregion

namespace BiteRight.Infrastructure.Common;

public class MediatorDomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : DomainEvent
{
    public MediatorDomainEventNotification(
        TDomainEvent domainEvent
    )
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}