// # ==============================================================================
// # Solution: BiteRight
// # File: Language.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Languages;

public class Language : AggregateRoot<LanguageId>
{
    // EF Core
    private Language()
    {
        NativeName = default!;
        EnglishName = default!;
        Code = default!;
    }

    private Language(
        LanguageId id,
        Name nativeName,
        Name englishName,
        Code code
    )
        : base(id)
    {
        NativeName = nativeName;
        EnglishName = englishName;
        Code = code;
    }

    public Name NativeName { get; private set; }
    public Name EnglishName { get; private set; }

    public Code Code { get; private set; }

    public static Language Create(
        Name nativeName,
        Name englishName,
        Code code,
        LanguageId? id = null
    )
    {
        var language = new Language(
            id ?? new LanguageId(),
            nativeName,
            englishName,
            code
        );

        return language;
    }
}