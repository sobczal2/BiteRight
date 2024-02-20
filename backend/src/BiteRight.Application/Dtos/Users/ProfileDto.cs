// # ==============================================================================
// # Solution: BiteRight
// # File: ProfileDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Application.Dtos.Currencies;

#endregion

namespace BiteRight.Application.Dtos.Users;

public class ProfileDto
{
    public CurrencyDto Currency { get; set; }
    public string TimeZoneId { get; set; } = default!;
}