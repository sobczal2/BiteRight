using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Abstracts.Common;

public interface ILanguageProvider
{
    Task<Language> RequireCurrent(CancellationToken cancellationToken = default);
    Task<LanguageId> RequireCurrentId(CancellationToken cancellationToken = default);
}