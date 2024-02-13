// # ==============================================================================
// # Solution: BiteRight
// # File: CachedEfCoreCurrencyRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Currencies;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

#endregion

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreCurrencyRepository : ICurrencyRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;
    private readonly IMemoryCache _idCache;

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

    public async Task<Currency?> FindById(
        CurrencyId id,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = GetCacheKey(id);

        if (_idCache.TryGetValue(cacheKey, out Currency? cachedCurrency)) return cachedCurrency;

        var currency = await _appDbContext.Currencies
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (currency is not null)
        {
            _idCache.Set(cacheKey, currency, _cacheEntryOptions);
        }

        return currency;
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

    private static string GetCacheKey(
        CurrencyId id
    )
    {
        return $"Currency_Id_{id}";
    }
}