// # ==============================================================================
// # Solution: BiteRight
// # File: BusinessRuleDomainException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

namespace BiteRight.Domain.Common.Exceptions;

public class BusinessRuleDomainException : DomainException
{
    public BusinessRuleDomainException()
        : base("Business rule violation")
    {
    }
}