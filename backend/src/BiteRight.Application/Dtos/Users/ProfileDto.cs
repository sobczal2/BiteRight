// # ==============================================================================
// # Solution: BiteRight
// # File: ProfileDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Currencies;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Application.Dtos.Users;

public class ProfileDto
{
    public ProfileDto(
        CurrencyDto currency,
        string timeZoneId
    )
    {
        Currency = currency;
        TimeZoneId = timeZoneId;
    }

    public CurrencyDto Currency { get; set; }
    public string TimeZoneId { get; set; }

    public static ProfileDto FromDomain(
        Profile profile
    )
    {
        return new ProfileDto(
            CurrencyDto.FromDomain(profile.Currency),
            profile.TimeZone.Id
        );
    }
}