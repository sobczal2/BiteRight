// # ==============================================================================
// # Solution: BiteRight
// # File: CachedEfCoreUnitRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Units;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

#endregion

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreUnitRepository : IUnitRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreUnitRepository(
        AppDbContext appDbContext,
        IMemoryCache cache,
        IOptions<CacheOptions> cacheOptions
    )
    {
        _appDbContext = appDbContext;
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(
            cacheOptions.Value.CategoryCacheDuration
        );
    }

    public async Task<(IEnumerable<Unit> Units, int TotalCount)> Search(
        string query,
        int pageNumber,
        int pageSize,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<Unit> baseQuery = _appDbContext.Units
            .Include(unit =>
                unit
                    .Translations
                    .Where(
                        translation => Equals(translation.LanguageId, languageId)
                    )
            );

        if (!string.IsNullOrWhiteSpace(query))
            baseQuery = baseQuery.Where(
                unit => unit
                    .Translations
                    .Any(translation =>
#pragma warning disable CA1862
                        (
                            ((string)translation.Name).ToLower().Contains(query.ToLower())
                            || ((string)translation.Abbreviation).ToLower().Contains(query.ToLower())
                        )
#pragma warning restore CA1862
                        && Equals(translation.LanguageId, languageId)
                    )
            );

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var units = await baseQuery
            .OrderBy(unit =>
                (string)unit.Translations.First(translation => translation.LanguageId == languageId).Name
            )
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (units, totalCount);
    }

    public async Task<Unit?> FindById(
        UnitId id,
        LanguageId languageId,
        CancellationToken cancellationToken
    )
    {
        var cacheKey = GetCacheKey(id, languageId);

        if (_cache.TryGetValue(cacheKey, out Unit? cachedUnit)) return cachedUnit;

        var unit = await _appDbContext.Units
            .Include(u => u.Translations.Where(t => t.LanguageId == languageId))
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (unit is not null)
        {
            _cache.Set(cacheKey, unit, _cacheEntryOptions);
        }

        return unit;
    }

    private static string GetCacheKey(
        UnitId id,
        LanguageId languageId
    )
    {
        return $"Unit_Id_{id}_Language_{languageId}";
    }
}