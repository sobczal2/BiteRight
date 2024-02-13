// # ==============================================================================
// # Solution: BiteRight
// # File: CachedEfCoreCountryRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Countries;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

#endregion

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreCountryRepository : ICountryRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreCountryRepository(
        AppDbContext appDbContext,
        IMemoryCache cache,
        IOptions<CacheOptions> cacheOptions
    )
    {
        _appDbContext = appDbContext;
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(
            cacheOptions.Value.CountryCacheDuration
        );
    }

    public async Task<Country?> FindById(
        CountryId id,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = GetCacheKey(id);

        if (_cache.TryGetValue(cacheKey, out Country? cachedCountry)) return cachedCountry;

        var country = await _appDbContext.Countries
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (country is not null)
        {
            _cache.Set(cacheKey, country, _cacheEntryOptions);
        }

        return country;
    }


    public async Task<bool> ExistsById(
        CountryId id,
        CancellationToken cancellationToken = default
    )
    {
        return await _appDbContext.Countries
            .AnyAsync(country => country.Id == id, cancellationToken);
    }

    private static string GetCacheKey(
        CountryId id
    )
    {
        return $"Country_Id_{id}";
    }
}