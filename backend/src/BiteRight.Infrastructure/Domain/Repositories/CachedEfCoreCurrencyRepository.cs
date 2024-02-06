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
    private readonly IMemoryCache _codeCache;
    private readonly IMemoryCache _idCache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreCurrencyRepository(
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
            cacheOptions.Value.CurrencyCacheDuration
        );
    }

    public async Task<Currency?> FindById(
        CurrencyId id,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = $"Currency_Id_{id}";

        if (_idCache.TryGetValue(cacheKey, out Currency? cachedCurrency)) return cachedCurrency;

        cachedCurrency = await _appDbContext.Currencies
            .FirstOrDefaultAsync(currency => currency.Id == id, cancellationToken);

        if (cachedCurrency != null)
        {
            _idCache.Set(cacheKey, cachedCurrency, _cacheEntryOptions);
        }

        return cachedCurrency;
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