using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Abstracts.Repositories;

public interface ILanguageRepository
{
    Task<Language?> FindByCode(
        Code code,
        CancellationToken cancellationToken = default
    );
}