using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Languages;

public class Language : AggregateRoot<LanguageId>
{
    public Name NativeName { get; }
    public Name EnglishName { get; }
    public Code Code { get; }

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
        IDomainEventFactory domainEventFactory,
        LanguageId? id = null
    )
    {
        var language = new Language(
            id ?? new LanguageId(),
            nativeName,
            englishName,
            code
        );

        language.AddDomainEvent(
            domainEventFactory.CreateLanguageCreatedEvent(
                language.Id
            )
        );

        return language;
    }
}