using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Languages.Exceptions;

public class CodeInvalidLengthException : BusinessRuleDomainException
{
    public int ExpectedLength { get; }
    
    public CodeInvalidLengthException(
        int expectedLength
    )
    {
        ExpectedLength = expectedLength;
    }
}