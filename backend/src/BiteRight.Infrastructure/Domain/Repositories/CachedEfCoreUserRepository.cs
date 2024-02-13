// # ==============================================================================
// # Solution: BiteRight
// # File: CachedEfCoreUserRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Users;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

#endregion

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
        var cacheKey = GetCacheKey(identityId);

        if (_cache.TryGetValue(cacheKey, out User? cachedUser)) return cachedUser;

        var user = await _appDbContext.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.IdentityId == identityId, cancellationToken);

        if (user is not null)
        {
            _cache.Set(cacheKey, user, _cacheEntryOptions);
        }

        return user;
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

    private static string GetCacheKey(
        IdentityId identityId
    )
    {
        return $"User_IdentityId_{identityId}";
    }
}