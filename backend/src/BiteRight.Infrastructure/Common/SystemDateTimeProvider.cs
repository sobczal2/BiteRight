using BiteRight.Domain.Abstracts;
using BiteRight.Domain.Abstracts.Common;

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