// # ==============================================================================
// # Solution: BiteRight
// # File: ICorrelationIdAccessor.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Domain.Abstracts.Common;

public interface ICorrelationIdAccessor
{
    Guid CorrelationId { get; }
}