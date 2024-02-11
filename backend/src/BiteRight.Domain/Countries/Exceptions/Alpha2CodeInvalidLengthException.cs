using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Countries.Exceptions;

public class Alpha2CodeInvalidLengthException : BusinessRuleDomainException
{
    public Alpha2CodeInvalidLengthException(
        int expectedLength
    )
    {
        ExpectedLength = expectedLength;
    }

    public int ExpectedLength { get; }
}