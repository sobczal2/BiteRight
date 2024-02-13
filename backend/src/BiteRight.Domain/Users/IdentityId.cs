// # ==============================================================================
// # Solution: BiteRight
// # File: IdentityId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Users;

public class IdentityId : Id<string>
{
    private IdentityId(
        string value
    )
        : base(value)
    {
    }

    public static IdentityId Create(
        string value
    )
    {
        return new IdentityId(value);
    }

    public static implicit operator string(
        IdentityId id
    )
    {
        return id.Value;
    }

    public static implicit operator IdentityId(
        string id
    )
    {
        return new IdentityId(id);
    }
}