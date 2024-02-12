// # ==============================================================================
// # Solution: BiteRight
// # File: IDomainEventHolder.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;

#endregion

namespace BiteRight.Domain.Common;

public interface IDomainEventHolder
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}