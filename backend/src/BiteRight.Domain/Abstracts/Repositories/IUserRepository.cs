// # ==============================================================================
// # Solution: BiteRight
// # File: IUserRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Domain.Abstracts.Repositories;

public interface IUserRepository
{
    void Add(User user);

    Task<User?> FindByIdentityId(
        IdentityId identityId,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByEmail(
        Email email,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByUsername(
        Username username,
        CancellationToken cancellationToken = default
    );
}