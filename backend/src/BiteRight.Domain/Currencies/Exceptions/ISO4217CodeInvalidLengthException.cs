using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Currencies.Exceptions;

public class ISO4217CodeInvalidLengthException : BusinessRuleDomainException
{
    public int ExpectedLength { get; }
    
    public ISO4217CodeInvalidLengthException(
        int expectedLength
    )
    {
        ExpectedLength = expectedLength;
    }
}