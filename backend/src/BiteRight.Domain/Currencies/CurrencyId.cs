// # ==============================================================================
// # Solution: BiteRight
// # File: CurrencyId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Currencies;

public class CurrencyId : GuidId
{
    public CurrencyId()
    {
    }

    public CurrencyId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        CurrencyId id
    )
    {
        return id.Value;
    }

    public static implicit operator CurrencyId(
        Guid id
    )
    {
        return new CurrencyId(id);
    }
}