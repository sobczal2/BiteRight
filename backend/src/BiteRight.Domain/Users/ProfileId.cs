using System;
using BiteRight.Domain.Common;

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