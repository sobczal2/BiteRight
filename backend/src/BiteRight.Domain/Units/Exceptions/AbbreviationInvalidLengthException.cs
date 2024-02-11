using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Units.Exceptions;

public class AbbreviationInvalidLengthException : BusinessRuleDomainException
{
    public int MinLength { get; }
    public int MaxLength { get; }

    public AbbreviationInvalidLengthException(
        int minLength,
        int maxLength
    )
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }
}