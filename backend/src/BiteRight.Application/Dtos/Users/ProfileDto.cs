// # ==============================================================================
// # Solution: BiteRight
// # File: ProfileDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Application.Dtos.Users;

public class ProfileDto
{
    public Guid CurrencyId { get; set; }
    public string CurrencyName { get; set; } = default!;
}