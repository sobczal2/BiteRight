namespace BiteRight.Domain.Abstracts.Common;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTimeOffset OffsetUtcNow { get; }
}