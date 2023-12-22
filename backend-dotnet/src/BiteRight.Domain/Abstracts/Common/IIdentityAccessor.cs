using BiteRight.Domain.Users;

namespace BiteRight.Domain.Abstracts.Common;

public interface IIdentityAccessor
{
    IdentityId? Current { get; }
    IdentityId RequireCurrent();
}