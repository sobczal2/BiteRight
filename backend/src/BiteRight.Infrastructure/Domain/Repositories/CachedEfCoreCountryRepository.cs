using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Countries;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreCountryRepository : ICountryRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _codeCache;
    private readonly IMemoryCache _idCache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreCountryRepository(
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

    public async Task<Country?> FindById(
        CountryId id,
        CancellationToken cancellationToken = default
    )
    {
        if (_idCache.TryGetValue(id, out Country? countryToFind)) return countryToFind;
        countryToFind = await _appDbContext.Countries
            .Where(country => country.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (countryToFind == null) return countryToFind;

        _idCache.Set(id, countryToFind, _cacheEntryOptions);

        return countryToFind;
    }

    public Task<bool> ExistsById(
        CountryId id,
        CancellationToken cancellationToken = default
    )
    {
        return _appDbContext.Countries
            .Where(country => country.Id == id)
            .AnyAsync(cancellationToken);
    }
}