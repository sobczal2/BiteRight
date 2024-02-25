// # ==============================================================================
// # Solution: BiteRight
// # File: CountryDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Countries;

#endregion

namespace BiteRight.Application.Dtos.Countries;

public class CountryDto
{
    public Guid Id { get; set; }
    public string NativeName { get; set; }
    public string EnglishName { get; set; }
    public string Alpha2Code { get; set; }
    public Guid OfficialLanguageId { get; set; }
    public Guid CurrencyId { get; set; }

    public CountryDto(
        Guid id,
        string nativeName,
        string englishName,
        string alpha2Code,
        Guid officialLanguageId,
        Guid currencyId
    )
    {
        Id = id;
        NativeName = nativeName;
        EnglishName = englishName;
        Alpha2Code = alpha2Code;
        OfficialLanguageId = officialLanguageId;
        CurrencyId = currencyId;
    }

    public static CountryDto FromDomain(
        Country country
    )
    {
        return new CountryDto(
            country.Id,
            country.NativeName,
            country.EnglishName,
            country.Alpha2Code,
            country.OfficialLanguageId,
            country.CurrencyId
        );
    }
}