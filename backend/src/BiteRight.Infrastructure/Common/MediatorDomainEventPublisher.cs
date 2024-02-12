// # ==============================================================================
// # Solution: BiteRight
// # File: MediatorDomainEventPublisher.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Common;
using MediatR;

#endregion

namespace BiteRight.Infrastructure.Common;

public class MediatorDomainEventPublisher : IDomainEventPublisher
{
    private readonly IMediator _mediator;

    public MediatorDomainEventPublisher(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    public Task PublishAsync<T>(
        T domainEvent,
        CancellationToken cancellationToken = default
    ) where T : DomainEvent
    {
        var notification = new MediatorDomainEventNotification<T>(domainEvent);
        return _mediator.Publish(notification, cancellationToken);
    }
}