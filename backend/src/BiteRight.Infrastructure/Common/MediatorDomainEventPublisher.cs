using BiteRight.Domain.Common;
using MediatR;

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
        return _mediator.Publish(domainEvent, cancellationToken);
    }
}