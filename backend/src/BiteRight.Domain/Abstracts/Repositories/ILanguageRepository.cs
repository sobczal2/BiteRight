using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Languages;

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