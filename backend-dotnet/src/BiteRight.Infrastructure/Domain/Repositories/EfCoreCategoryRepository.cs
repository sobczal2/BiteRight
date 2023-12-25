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
        var categories = await _appDbContext
            .Database
            .SqlQuery<Category>(
                $"""
                 SELECT c.id, ct.name, ct.photo FROM categories c
                 LEFT JOIN category_translations ct ON c.id = ct.category_id
                 WHERE ct.language_id = {languageId.Value}
                    AND ct.name ILIKE '%{name}%'
                 LIMIT {pageSize}
                 OFFSET {pageNumber * pageSize}
                 """
            )
            .ToListAsync(cancellationToken);
        
        var totalCount = await _appDbContext
            .Database
            .SqlQuery<int>(
                $"""
                 SELECT COUNT(*) FROM categories c
                 LEFT JOIN category_translations ct ON c.id = ct.category_id
                 WHERE ct.language_id = {languageId.Value}
                    AND ct.name ILIKE '%{name}%'
                 """
                ).FirstOrDefaultAsync(cancellationToken);
        
        return (categories, totalCount);
    }
}