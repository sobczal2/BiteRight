// # ==============================================================================
// # Solution: BiteRight
// # File: LanguageDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Languages;

#endregion

namespace BiteRight.Application.Dtos.Languages;

public class LanguageDto
{
    public Guid Id { get; set; }
    public string EnglishName { get; set; }
    public string Code { get; set; }

    public LanguageDto(
        Guid id,
        string englishName,
        string code
    )
    {
        Id = id;
        EnglishName = englishName;
        Code = code;
    }

    public static LanguageDto FromDomain(
        Language language
    )
    {
        return new LanguageDto(
            language.Id,
            language.EnglishName,
            language.Code
        );
    }
}