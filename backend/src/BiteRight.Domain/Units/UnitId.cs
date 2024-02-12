// # ==============================================================================
// # Solution: BiteRight
// # File: UnitId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Units;

public class UnitId : GuidId
{
    public UnitId()
    {
    }

    public UnitId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        UnitId id
    )
    {
        return id.Value;
    }

    public static implicit operator UnitId(
        Guid id
    )
    {
        return new UnitId(id);
    }
}