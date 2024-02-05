using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Languages;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Infrastructure.Domain.Repositories;

public class EfCoreCategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _appDbContext;

    public EfCoreCategoryRepository(
        AppDbContext appDbContext
    )
    {
        _appDbContext = appDbContext;
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
        Guid id,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    )
    {
        return await _appDbContext.Categories
            .Include(category =>
                category.Translations.Where(translation => Equals(translation.LanguageId, languageId)))
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }
}