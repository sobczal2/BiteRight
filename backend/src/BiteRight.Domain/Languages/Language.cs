using BiteRight.Domain.Common;

namespace BiteRight.Domain.Languages;

public class Language : AggregateRoot<LanguageId>
{
    public Name NativeName { get; private set; }
    public Name EnglishName { get; private set; }

    public Code Code { get; private set; }

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