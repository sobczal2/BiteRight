// # ==============================================================================
// # Solution: BiteRight
// # File: InternalDomainException.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

namespace BiteRight.Domain.Common.Exceptions;

public class InternalDomainException : DomainException
{
    public InternalDomainException(
        string message
    )
        : base(message)
    {
    }
}