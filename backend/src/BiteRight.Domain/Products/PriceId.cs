// # ==============================================================================
// # Solution: BiteRight
// # File: PriceId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Products;

public class PriceId : GuidId
{
    public PriceId()
    {
    }

    public PriceId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        PriceId id
    )
    {
        return id.Value;
    }

    public static implicit operator PriceId(
        Guid id
    )
    {
        return new PriceId(id);
    }
}