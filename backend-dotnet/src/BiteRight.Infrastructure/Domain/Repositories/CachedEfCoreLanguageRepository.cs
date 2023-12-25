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
        if (_cache.TryGetValue(code, out Language? languageToFind)) return languageToFind;
        languageToFind = await _appDbContext.Languages
            .Where(language => language.Code == code)
            .FirstOrDefaultAsync(cancellationToken);

        if (languageToFind == null) return languageToFind;

        _cache.Set(code, languageToFind, _cacheEntryOptions);

        return languageToFind;
    }
}