// # ==============================================================================
// # Solution: BiteRight
// # File: SystemDateTimeProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Abstracts.Common;

#endregion

namespace BiteRight.Infrastructure.Common;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
    public DateOnly Today => DateOnly.FromDateTime(UtcNow);

    public DateTime GetLocalTime(
        TimeZoneInfo timeZone
    )
    {
        return TimeZoneInfo.ConvertTimeFromUtc(UtcNow, timeZone);
    }

    public DateTimeOffset GetLocalOffsetTime(
        TimeZoneInfo timeZone
    )
    {
        return TimeZoneInfo.ConvertTimeFromUtc(UtcNow, timeZone);
    }

    public DateOnly GetLocalDate(
        TimeZoneInfo timeZone
    )
    {
        return DateOnly.FromDateTime(TimeZoneInfo.ConvertTimeFromUtc(UtcNow, timeZone));
    }
}