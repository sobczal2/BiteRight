// # ==============================================================================
// # Solution: BiteRight
// # File: IDateTimeProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Domain.Abstracts.Common;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTimeOffset OffsetUtcNow { get; }
    DateOnly Today { get; }

    DateTime GetLocalTime(
        TimeZoneInfo timeZone
    );

    DateTimeOffset GetLocalOffsetTime(
        TimeZoneInfo timeZone
    );

    DateOnly GetLocalDate(
        TimeZoneInfo timeZone
    );
}