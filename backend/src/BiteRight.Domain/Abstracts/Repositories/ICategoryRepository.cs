using BiteRight.Domain.Categories;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Abstracts.Repositories;

public interface ICategoryRepository
{
    Task<(IEnumerable<Category> Categories, int TotalCount)> Search(
        string query,
        int pageNumber,
        int pageSize,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    );

    Task<Category?> FindById(
        CategoryId id,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    );
}