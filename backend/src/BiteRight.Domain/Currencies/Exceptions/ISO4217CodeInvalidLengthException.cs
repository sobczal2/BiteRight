using BiteRight.Domain.Common.Exceptions;

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