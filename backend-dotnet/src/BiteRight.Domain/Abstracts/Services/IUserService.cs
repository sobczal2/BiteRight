using BiteRight.Domain.Users;

namespace BiteRight.Domain.Abstracts.Services;

public interface IUserService
{
    public Task<bool> IsEmailAvailable(
        Email email,
        CancellationToken cancellationToken = default
    );

    public Task<bool> IsUsernameAvailable(
        Username username,
        CancellationToken cancellationToken = default
    );
}