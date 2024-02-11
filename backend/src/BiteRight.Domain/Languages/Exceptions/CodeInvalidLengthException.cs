using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Languages.Exceptions;

public class CodeInvalidLengthException : BusinessRuleDomainException
{
    public CodeInvalidLengthException(
        int expectedLength
    )
    {
        ExpectedLength = expectedLength;
    }

    public int ExpectedLength { get; }
}