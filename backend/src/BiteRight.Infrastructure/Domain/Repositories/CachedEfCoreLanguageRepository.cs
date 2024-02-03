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
        if (_codeCache.TryGetValue(code, out Language? languageToFind)) return languageToFind;
        languageToFind = await _appDbContext.Languages
            .Where(language => language.Code == code)
            .FirstOrDefaultAsync(cancellationToken);

        if (languageToFind == null) return languageToFind;

        _codeCache.Set(code, languageToFind, _cacheEntryOptions);

        return languageToFind;
    }

    public async Task<Language?> FindById(
        LanguageId id,
        CancellationToken cancellationToken = default
    )
    {
        if (_idCache.TryGetValue(id, out Language? languageToFind)) return languageToFind;
        languageToFind = await _appDbContext.Languages
            .Where(language => language.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (languageToFind == null) return languageToFind;

        _idCache.Set(id, languageToFind, _cacheEntryOptions);

        return languageToFind;
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