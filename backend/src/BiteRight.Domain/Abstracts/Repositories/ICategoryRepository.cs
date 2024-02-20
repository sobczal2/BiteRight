// # ==============================================================================
// # Solution: BiteRight
// # File: ICategoryRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Languages;

#endregion

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

    Task<Category> GetDefault(
        LanguageId languageId,
        CancellationToken cancellationToken
    );
}