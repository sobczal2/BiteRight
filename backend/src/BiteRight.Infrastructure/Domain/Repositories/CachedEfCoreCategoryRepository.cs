// # ==============================================================================
// # Solution: BiteRight
// # File: CachedEfCoreCategoryRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Languages;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

#endregion

namespace BiteRight.Infrastructure.Domain.Repositories;

public class CachedEfCoreCategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public CachedEfCoreCategoryRepository(
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

    public async Task<(IEnumerable<Category> Categories, int TotalCount)> Search(
        string query,
        int pageNumber,
        int pageSize,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<Category> baseQuery = _appDbContext.Categories
            .Include(category =>
                category.Translations.Where(translation => Equals(translation.LanguageId, languageId)));

        if (!string.IsNullOrWhiteSpace(query))
            baseQuery = baseQuery.Where(category =>
                category.Translations.Any(translation =>
#pragma warning disable CA1862
                    ((string)translation.Name).ToLower().Contains(query.ToLower())
#pragma warning restore CA1862
                    && Equals(translation.LanguageId, languageId)
                )
            );

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var categories = await baseQuery
            .OrderBy(category =>
                (string)category.Translations.First(translation => translation.LanguageId == languageId).Name)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (categories, totalCount);
    }

    public async Task<Category?> FindById(
        CategoryId id,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = GetCacheKey(id, languageId);

        if (_cache.TryGetValue(cacheKey, out Category? cachedCategory)) return cachedCategory;

        var category = await _appDbContext.Categories
            .Include(c => c.Translations.Where(t => t.LanguageId == languageId))
            .Include(c => c.Photo)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (category is not null) _cache.Set(cacheKey, category, _cacheEntryOptions);

        return category;
    }

    public async Task<Category> GetDefault(
        LanguageId languageId,
        CancellationToken cancellationToken
    )
    {
        return await _appDbContext.Categories
            .Include(c => c.Translations.Where(t => t.LanguageId == languageId))
            .Include(c => c.Photo)
            .SingleAsync(c => c.IsDefault, cancellationToken);
    }


    private static string GetCacheKey(
        CategoryId id,
        LanguageId languageId
    )
    {
        return $"Category_Id_{id}_Language_{languageId}";
    }
}