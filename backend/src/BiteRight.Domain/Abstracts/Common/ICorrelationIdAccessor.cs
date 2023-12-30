namespace BiteRight.Domain.Abstracts.Common;

public interface ICorrelationIdAccessor
{
    Guid CorrelationId { get; }
}