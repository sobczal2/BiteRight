// # ==============================================================================
// # Solution: BiteRight
// # File: CountryDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Application.Dtos.Countries;

public class CountryDto
{
    public Guid Id { get; set; }
    public string NativeName { get; set; } = default!;
    public string EnglishName { get; set; } = default!;
    public string Alpha2Code { get; set; } = default!;
    public Guid OfficialLanguageId { get; set; }
    public Guid CurrencyId { get; set; }
}