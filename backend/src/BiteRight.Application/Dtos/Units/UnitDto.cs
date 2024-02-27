// # ==============================================================================
// # Solution: BiteRight
// # File: UnitDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Units;

#endregion

namespace BiteRight.Application.Dtos.Units;

public class UnitDto
{
    public UnitDto(
        Guid id,
        string name,
        string abbreviation,
        UnitSystemDto unitSystem
    )
    {
        Id = id;
        Name = name;
        Abbreviation = abbreviation;
        UnitSystem = unitSystem;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public UnitSystemDto UnitSystem { get; set; }

    public static UnitDto FromDomain(
        Unit unit,
        LanguageId languageId
    )
    {
        return new UnitDto(
            unit.Id,
            unit.GetName(languageId),
            unit.GetAbbreviation(languageId),
            (UnitSystemDto)unit.UnitSystem
        );
    }
}