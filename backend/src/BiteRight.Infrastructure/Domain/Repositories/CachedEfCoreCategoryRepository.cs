using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Languages;
using BiteRight.Infrastructure.Database;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

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
        string name,
        int pageNumber,
        int pageSize,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<Category> query = _appDbContext.Categories
            .Include(category =>
                category.Translations.Where(translation => Equals(translation.LanguageId, languageId)));

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(category =>
                category.Translations.Any(translation =>
#pragma warning disable CA1862
                    ((string)translation.Name).ToLower().Contains(name.ToLower())
#pragma warning restore CA1862
                    && Equals(translation.LanguageId, languageId)
                )
            );
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var categories = await query
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
        var cacheKey = $"Category_Id_{id}_Language_{languageId}";

        if (_cache.TryGetValue(cacheKey, out Category? cachedCategory)) return cachedCategory;

        cachedCategory = await _appDbContext.Categories
            .Include(category => category.Translations.Where(translation => translation.LanguageId == languageId))
            .Include(category => category.Photo)
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);

        if (cachedCategory != null)
        {
            _cache.Set(cacheKey, cachedCategory, _cacheEntryOptions);
        }

        return cachedCategory;
    }

}