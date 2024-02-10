using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Currencies;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreCurrencyRepository : ICurrencyRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _idCache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreCurrencyRepository(
        AppDbContext appDbContext,
        IMemoryCache idCache,
        IOptions<CacheOptions> cacheOptions
    )
    {
        _appDbContext = appDbContext;
        _idCache = idCache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(
            cacheOptions.Value.CurrencyCacheDuration
        );
    }
    
    private static string GetCacheKey(
        CurrencyId id
    )
    {
        return $"Currency_Id_{id}";
    }

    public async Task<Currency?> FindById(
        CurrencyId id,
        CancellationToken cancellationToken = default
    )
    {
        return await _idCache.GetOrCreateAsync(
            GetCacheKey(id),
            async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await _appDbContext.Currencies
                    .FirstOrDefaultAsync(currency => currency.Id == id, cancellationToken);
            }
        );
    }


    public Task<bool> ExistsById(
        CurrencyId id,
        CancellationToken cancellationToken = default
    )
    {
        return _appDbContext.Currencies
            .Where(currency => currency.Id == id)
            .AnyAsync(cancellationToken);
    }
}