using BiteRight.Domain.Abstracts.Services;
using BiteRight.Domain.Users;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Infrastructure.Domain;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(
        AppDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsEmailAvailable(
        Email email
    )
    {
        return !await _dbContext.Users
            .AnyAsync(user => user.Email == email);
    }

    public async Task<bool> IsUsernameAvailable(
        Username username
    )
    {
        return !await _dbContext.Users
            .AnyAsync(user => user.Username == username);
    }
}