// # ==============================================================================
// # Solution: BiteRight
// # File: IIdentityManager.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Domain.Abstracts.Common;

public interface IIdentityManager
{
    Task<(Email email, bool isVerified)> GetEmail(
        IdentityId identityId,
        CancellationToken cancellationToken = default
    );
}