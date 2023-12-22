using BiteRight.Domain.Common;

namespace BiteRight.Domain.Users;

public class UserId : GuidId
{
    public UserId(
        Guid value
    )
        : base(value)
    {
    }
}