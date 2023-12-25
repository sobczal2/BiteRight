using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Languages;
using Microsoft.AspNetCore.Http;

namespace BiteRight.Web.Providers;

public class CultureLanguageProvider : ILanguageProvider
{
    private readonly ICultureProvider _cultureProvider;
    private readonly ILanguageRepository _languageRepository;

    public CultureLanguageProvider(
        ICultureProvider cultureProvider,
        ILanguageRepository languageRepository
    )
    {
        _cultureProvider = cultureProvider;
        _languageRepository = languageRepository;
    }

    public async Task<Language> RequireCurrent(
        CancellationToken cancellationToken = default
    )
    {
        var code = GetLanguageCode();

        if (code is null)
        {
            throw new InvalidOperationException("Language code is null");
        }

        var language = await _languageRepository.FindByCode(code, cancellationToken) ??
                       await _languageRepository.FindByCode(Code.Default, cancellationToken);

        if (language is null)
        {
            throw new InvalidOperationException("Language is null");
        }

        return language;
    }

    public async Task<LanguageId> RequireCurrentId(
        CancellationToken cancellationToken = default
    )
    {
        return (await RequireCurrent(cancellationToken)).Id;
    }

    private Code GetLanguageCode()
    {
        return Code.Create(_cultureProvider.RequireCurrent().TwoLetterISOLanguageName);
    }
}