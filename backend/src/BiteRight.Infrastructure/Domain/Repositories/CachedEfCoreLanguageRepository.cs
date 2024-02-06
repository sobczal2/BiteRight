using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Languages;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreLanguageRepository : ILanguageRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _codeCache;
    private readonly IMemoryCache _idCache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreLanguageRepository(
        AppDbContext appDbContext,
        IMemoryCache codeCache,
        IMemoryCache idCache,
        IOptions<CacheOptions> cacheOptions
    )
    {
        _appDbContext = appDbContext;
        _codeCache = codeCache;
        _idCache = idCache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(
            cacheOptions.Value.LanguageCacheDuration
        );
    }

    public async Task<Language?> FindByCode(
        Code code,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = $"Language_Code_{code}";

        if (_codeCache.TryGetValue(cacheKey, out Language? cachedLanguage)) return cachedLanguage;

        cachedLanguage = await _appDbContext.Languages
            .FirstOrDefaultAsync(language => language.Code == code, cancellationToken);

        if (cachedLanguage != null)
        {
            _codeCache.Set(cacheKey, cachedLanguage, _cacheEntryOptions);
        }

        return cachedLanguage;
    }


    public async Task<Language?> FindById(
        LanguageId id,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = $"Language_Id_{id}";

        if (_idCache.TryGetValue(cacheKey, out Language? cachedLanguage)) return cachedLanguage;

        cachedLanguage = await _appDbContext.Languages
            .FirstOrDefaultAsync(language => language.Id == id, cancellationToken);

        if (cachedLanguage != null)
        {
            _idCache.Set(cacheKey, cachedLanguage, _cacheEntryOptions);
        }

        return cachedLanguage;
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
}