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
            cacheOptions.Value.LanguageCacheDuration
        );
    }

    public async Task<Currency?> FindById(
        CurrencyId id,
        CancellationToken cancellationToken = default
    )
    {
        if (_idCache.TryGetValue(id, out Currency? currencyToFind)) return currencyToFind;
        currencyToFind = await _appDbContext.Currencies
            .Where(currency => currency.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (currencyToFind == null) return currencyToFind;

        _idCache.Set(id, currencyToFind, _cacheEntryOptions);

        return currencyToFind;
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