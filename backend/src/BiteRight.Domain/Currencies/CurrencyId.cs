using System;
using BiteRight.Domain.Common;

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