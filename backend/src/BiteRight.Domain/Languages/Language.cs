using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Languages;

public class Language : AggregateRoot<LanguageId>
{
    public Name Name { get; }
    public Code Code { get; }

    // EF Core
    private Language()
    {
        Name = default!;
        Code = default!;
    }

    private Language(
        LanguageId id,
        Name name,
        Code code
    )
        : base(id)
    {
        Name = name;
        Code = code;
    }

    public static Language Create(
        Name name,
        Code code,
        IDomainEventFactory domainEventFactory,
        LanguageId? id = null
    )
    {
        var language = new Language(
            id ?? new LanguageId(),
            name,
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