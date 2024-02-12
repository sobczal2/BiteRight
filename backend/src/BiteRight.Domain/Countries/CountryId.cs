// # ==============================================================================
// # Solution: BiteRight
// # File: CountryId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Countries;

public class CountryId : GuidId
{
    public CountryId()
    {
    }

    public CountryId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        CountryId id
    )
    {
        return id.Value;
    }

    public static implicit operator CountryId(
        Guid id
    )
    {
        return new CountryId(id);
    }
}