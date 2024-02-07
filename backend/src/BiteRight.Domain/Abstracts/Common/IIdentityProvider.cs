using BiteRight.Domain.Users;

namespace BiteRight.Domain.Abstracts.Common;

public interface IIdentityProvider
{
    IdentityId RequireCurrent();
    Task<UserId> RequireCurrentUserId();
}