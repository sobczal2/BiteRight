using System;

namespace BiteRight.Domain.Abstracts.Common;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTimeOffset OffsetUtcNow { get; }
    DateOnly Today { get; }
    DateTime GetLocalTime(TimeZoneInfo timeZone);
    DateTimeOffset GetLocalOffsetTime(TimeZoneInfo timeZone);
    DateOnly GetLocalDate(TimeZoneInfo timeZone);
}