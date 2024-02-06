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
    private readonly IMemoryCache _idCache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreCountryRepository(
        AppDbContext appDbContext,
        IMemoryCache idCache,
        IOptions<CacheOptions> cacheOptions
    )
    {
        _appDbContext = appDbContext;
        _idCache = idCache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(
            cacheOptions.Value.CountryCacheDuration
        );
    }

    public async Task<Country?> FindById(
        CountryId id,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = $"Country_Id_{id}";

        if (_idCache.TryGetValue(cacheKey, out Country? cachedCountry)) return cachedCountry;

        cachedCountry = await _appDbContext.Countries
            .FirstOrDefaultAsync(country => country.Id == id, cancellationToken);

        if (cachedCountry != null)
        {
            _idCache.Set(cacheKey, cachedCountry, _cacheEntryOptions);
        }

        return cachedCountry;
    }


    public async Task<bool> ExistsById(
        CountryId id,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = $"Country_Exists_Id_{id}";

        if (_idCache.TryGetValue(cacheKey, out bool exists)) return exists;

        exists = await _appDbContext.Countries
            .AnyAsync(country => country.Id == id, cancellationToken);

        _idCache.Set(cacheKey, exists, _cacheEntryOptions);

        return exists;
    }

}