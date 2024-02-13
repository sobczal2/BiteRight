// # ==============================================================================
// # Solution: BiteRight
// # File: ProfileId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Users;

public class ProfileId : GuidId
{
    public ProfileId()
    {
    }

    public ProfileId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        ProfileId id
    )
    {
        return id.Value;
    }

    public static implicit operator ProfileId(
        Guid id
    )
    {
        return new ProfileId(id);
    }
}