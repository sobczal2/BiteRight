// # ==============================================================================
// # Solution: BiteRight
// # File: UserId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Users;

public class UserId : GuidId
{
    public UserId()
    {
    }

    public UserId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        UserId id
    )
    {
        return id.Value;
    }

    public static implicit operator UserId(
        Guid id
    )
    {
        return new UserId(id);
    }
}