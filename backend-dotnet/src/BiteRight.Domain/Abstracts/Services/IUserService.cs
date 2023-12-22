using BiteRight.Domain.Users;

namespace BiteRight.Domain.Abstracts.Services;

public interface IUserService
{
    public Task<bool> IsEmailAvailable(
        Email email
    );
    
    public Task<bool> IsUsernameAvailable(
        Username username
    );
}