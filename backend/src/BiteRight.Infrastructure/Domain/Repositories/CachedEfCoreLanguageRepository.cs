// # ==============================================================================
// # Solution: BiteRight
// # File: CachedEfCoreLanguageRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Languages;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

#endregion

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreLanguageRepository : ILanguageRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreLanguageRepository(
        AppDbContext appDbContext,
        IMemoryCache cache,
        IOptions<CacheOptions> cacheOptions
    )
    {
        _appDbContext = appDbContext;
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(
            cacheOptions.Value.LanguageCacheDuration
        );
    }

    public async Task<Language?> FindByCode(
        Code code,
        CancellationToken cancellationToken = default
    )
    {
        return await _cache.GetOrCreateAsync(
            GetCacheKey(code),
            async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await _appDbContext.Languages
                    .FirstOrDefaultAsync(language => language.Code == code, cancellationToken);
            }
        );
    }


    public async Task<Language?> FindById(
        LanguageId id,
        CancellationToken cancellationToken = default
    )
    {
        return await _cache.GetOrCreateAsync(
            GetCacheKey(id),
            async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await _appDbContext.Languages
                    .FirstOrDefaultAsync(language => language.Id == id, cancellationToken);
            }
        );
    }


    public Task<bool> ExistsById(
        LanguageId id,
        CancellationToken cancellationToken = default
    )
    {
        return _appDbContext.Languages
            .Where(language => language.Id == id)
            .AnyAsync(cancellationToken);
    }

    private static string GetCacheKey(
        LanguageId id
    )
    {
        return $"Language_Id_{id}";
    }

    private static string GetCacheKey(
        Code code
    )
    {
        return $"Language_Code_{code}";
    }
}