using System;
using System.Collections.Generic;
using System.Linq;
using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Units;

public class Unit : AggregateRoot<UnitId>
{
    // EF Core
    private Unit()
    {
        UnitSystem = default!;
        Translations = default!;
    }

    private Unit(
        UnitId id,
        UnitSystem unitSystem
    ) : base(id)
    {
        UnitSystem = unitSystem;
        Translations = default!;
    }

    public UnitSystem UnitSystem { get; private set; }

    public IEnumerable<Translation> Translations { get; }

    public static Unit Create(
        UnitSystem unitSystem,
        UnitId? id = null
    )
    {
        var unit = new Unit(
            id ?? new UnitId(),
            unitSystem
        );

        return unit;
    }

    public string GetName(LanguageId languageId)
    {
        return Translations
            .SingleOrDefault(t => Equals(t.LanguageId, languageId))
            ?.Name ?? throw new InvalidOperationException();
    }

    public string GetAbbreviation(LanguageId languageId)
    {
        return Translations
            .SingleOrDefault(t => Equals(t.LanguageId, languageId))
            ?.Abbreviation ?? throw new InvalidOperationException();
    }
}