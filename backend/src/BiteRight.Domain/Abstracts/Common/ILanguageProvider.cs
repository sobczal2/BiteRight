// # ==============================================================================
// # Solution: BiteRight
// # File: ILanguageProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Languages;

#endregion

namespace BiteRight.Domain.Abstracts.Common;

public interface ILanguageProvider
{
    Task<Language> RequireCurrent(CancellationToken cancellationToken = default);
    Task<LanguageId> RequireCurrentId(CancellationToken cancellationToken = default);
}