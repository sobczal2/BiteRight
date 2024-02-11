using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Units;

public class Translation : Entity<TranslationId>
{
    // EF Core
    private Translation()
    {
        UnitId = default!;
        Unit = default!;
        LanguageId = default!;
        Language = default!;
        Name = default!;
        Abbreviation = default!;
    }

    private Translation(
        TranslationId id,
        UnitId unitId,
        LanguageId languageId,
        Name name,
        Abbreviation abbreviation
    )
        : base(id)
    {
        UnitId = unitId;
        Unit = default!;
        LanguageId = languageId;
        Language = default!;
        Name = name;
        Abbreviation = abbreviation;
    }

    public UnitId UnitId { get; private set; }
    public virtual Unit Unit { get; private set; }
    public LanguageId LanguageId { get; private set; }
    public virtual Language Language { get; private set; }
    public Name Name { get; private set; }
    public Abbreviation Abbreviation { get; private set; }

    public static Translation Create(
        UnitId unitId,
        LanguageId languageId,
        Name name,
        Abbreviation abbreviation,
        TranslationId? id = null
    )
    {
        return new Translation(
            id ?? new TranslationId(),
            unitId,
            languageId,
            name,
            abbreviation
        );
    }
}