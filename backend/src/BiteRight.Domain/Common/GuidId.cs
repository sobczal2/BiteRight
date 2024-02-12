// # ==============================================================================
// # Solution: BiteRight
// # File: GuidId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Domain.Common;

public abstract class GuidId : Id<Guid>
{
    protected GuidId()
        : base(Guid.NewGuid())
    {
    }

    protected GuidId(
        Guid value
    )
        : base(value)
    {
    }
}