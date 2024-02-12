// # ==============================================================================
// # Solution: BiteRight
// # File: ILanguageRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Languages;

#endregion

namespace BiteRight.Domain.Abstracts.Repositories;

public interface ILanguageRepository
{
    Task<Language?> FindByCode(
        Code code,
        CancellationToken cancellationToken = default
    );

    Task<Language?> FindById(
        LanguageId id,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsById(
        LanguageId id,
        CancellationToken cancellationToken = default
    );
}