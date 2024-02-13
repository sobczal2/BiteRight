// # ==============================================================================
// # Solution: BiteRight
// # File: ISO4217CodeInvalidLengthException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Currencies.Exceptions;

public class ISO4217CodeInvalidLengthException : BusinessRuleDomainException
{
    public ISO4217CodeInvalidLengthException(
        int expectedLength
    )
    {
        ExpectedLength = expectedLength;
    }

    public int ExpectedLength { get; }
}