using BiteRight.Domain.Common;
using MediatR;

namespace BiteRight.Infrastructure.Common;

public class MediatorDomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : DomainEvent
{
    public TDomainEvent DomainEvent { get; }

    public MediatorDomainEventNotification(
        TDomainEvent domainEvent
    )
    {
        DomainEvent = domainEvent;
    }
}