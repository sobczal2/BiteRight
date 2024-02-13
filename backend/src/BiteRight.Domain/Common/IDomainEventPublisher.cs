// # ==============================================================================
// # Solution: BiteRight
// # File: IDomainEventPublisher.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;

#endregion

namespace BiteRight.Domain.Common;

public interface IDomainEventPublisher
{
    Task PublishAsync<T>(T domainEvent, CancellationToken cancellationToken = default) where T : DomainEvent;
}