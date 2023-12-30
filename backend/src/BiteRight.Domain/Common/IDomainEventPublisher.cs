namespace BiteRight.Domain.Common;

public interface IDomainEventPublisher
{
    Task PublishAsync<T>(T domainEvent, CancellationToken cancellationToken = default) where T : DomainEvent;
}