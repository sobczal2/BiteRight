// # ==============================================================================
// # Solution: BiteRight
// # File: CurrencyDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Application.Dtos.Currencies;

public class CurrencyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string Code { get; set; } = default!;
}