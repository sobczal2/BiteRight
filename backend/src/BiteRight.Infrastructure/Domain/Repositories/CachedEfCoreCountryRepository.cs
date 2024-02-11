using System.Threading;
using System.Threading.Tasks;
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
        return await _cache.GetOrCreateAsync(
            GetCacheKey(id),
            async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await _appDbContext.Countries
                    .FirstOrDefaultAsync(country => country.Id == id, cancellationToken);
            }
        );
    }

    public async Task<bool> ExistsById(
        CountryId id,
        CancellationToken cancellationToken = default
    )
    {
        return await _cache.GetOrCreateAsync(
            GetCacheKey(id),
            async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await _appDbContext.Countries
                    .AnyAsync(country => country.Id == id, cancellationToken);
            }
        );
    }

    private static string GetCacheKey(
        CountryId id
    )
    {
        return $"Country_Id_{id}";
    }
}