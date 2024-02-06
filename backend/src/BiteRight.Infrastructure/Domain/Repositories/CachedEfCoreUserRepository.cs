using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Users;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreUserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreUserRepository(
        AppDbContext appDbContext,
        IMemoryCache cache,
        IOptions<CacheOptions> cacheOptions
    )
    {
        _appDbContext = appDbContext;
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(
            cacheOptions.Value.UserCacheDuration
        );
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
        var cacheKey = $"User_{identityId}";

        if (_cache.TryGetValue(cacheKey, out User? cachedUser)) return cachedUser;
        cachedUser = await _appDbContext
            .Users
            .Include(user => user.Profile)
            .FirstOrDefaultAsync(
                user => user.IdentityId == identityId,
                cancellationToken
            );

        if (cachedUser != null)
        {
            _cache.Set(cacheKey, cachedUser, _cacheEntryOptions);
        }

        return cachedUser;
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