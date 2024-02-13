// # ==============================================================================
// # Solution: BiteRight
// # File: IIdentityProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading.Tasks;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Domain.Abstracts.Common;

public interface IIdentityProvider
{
    IdentityId RequireCurrent();
    Task<UserId> RequireCurrentUserId();
    Task<User> RequireCurrentUser();
}