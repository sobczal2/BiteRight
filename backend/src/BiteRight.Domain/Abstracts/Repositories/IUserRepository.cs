using BiteRight.Domain.Users;

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