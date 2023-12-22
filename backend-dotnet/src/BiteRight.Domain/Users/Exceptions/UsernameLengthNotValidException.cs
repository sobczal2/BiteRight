using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Users.Exceptions;

public class UsernameLengthNotValidException : BusinessRuleDomainException
{
    public int MinLength { get; }
    public int MaxLength { get; }

    public UsernameLengthNotValidException(
        int minLength,
        int maxLength
    )
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }
}