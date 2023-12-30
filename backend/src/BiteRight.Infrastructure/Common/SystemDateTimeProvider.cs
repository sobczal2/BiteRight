using BiteRight.Domain.Abstracts;
using BiteRight.Domain.Abstracts.Common;

namespace BiteRight.Infrastructure.Common;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
}