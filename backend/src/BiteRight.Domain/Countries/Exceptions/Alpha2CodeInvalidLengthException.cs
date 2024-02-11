using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Countries.Exceptions;

public class Alpha2CodeInvalidLengthException : BusinessRuleDomainException
{
    public int ExpectedLength { get; }

    public Alpha2CodeInvalidLengthException(
        int expectedLength
    )
    {
        ExpectedLength = expectedLength;
    }
}