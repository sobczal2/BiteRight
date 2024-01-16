using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Users;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Infrastructure.Domain.Repositories;

public class EfCoreUserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public EfCoreUserRepository(
        AppDbContext appDbContext
    )
    {
        _appDbContext = appDbContext;
    }

    public void Add(
        User user
    )
    {
        _appDbContext.Users.Add(user);
    }

    public async Task<User?> FindByIdentityId(
        IdentityId identityId,
        CancellationToken cancellationToken = default
    )
    {
        return await _appDbContext
            .Users
            .Include(user => user.Profile)
            .FirstOrDefaultAsync(
                user => user.IdentityId == identityId,
                cancellationToken
            );
    }

    public async Task<bool> ExistsByEmail(
        Email email,
        CancellationToken cancellationToken = default
    )
    {
        return await _appDbContext
            .Users
            .AnyAsync(
                user => user.Email == email,
                cancellationToken
            );
    }

    public async Task<bool> ExistsByUsername(
        Username username,
        CancellationToken cancellationToken = default
    )
    {
        return await _appDbContext
            .Users
            .AnyAsync(
                user => user.Username == username,
                cancellationToken
            );
    }
}