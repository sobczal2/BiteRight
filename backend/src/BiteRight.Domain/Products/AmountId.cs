// # ==============================================================================
// # Solution: BiteRight
// # File: AmountId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Products;

public class AmountId : GuidId
{
    public AmountId()
    {
    }

    public AmountId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        AmountId id
    )
    {
        return id.Value;
    }

    public static implicit operator AmountId(
        Guid id
    )
    {
        return new AmountId(id);
    }
}