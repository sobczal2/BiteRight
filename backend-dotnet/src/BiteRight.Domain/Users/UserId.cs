using BiteRight.Domain.Common;

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
    ) => id.Value;

    public static implicit operator UserId(
        Guid id
    ) => new(id);
}