using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Users.Exceptions;

public class UsernameInvalidLengthException : BusinessRuleDomainException
{
    public UsernameInvalidLengthException(
        int minLength,
        int maxLength
    )
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }

    public int MinLength { get; }
    public int MaxLength { get; }
}