using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Users.Exceptions;

public class UsernameNotValidException : BusinessRuleDomainException
{
    public UsernameNotValidException(
        string message
    )
        : base(message)
    {
    }
    
    public static UsernameNotValidException CreateInvalidLength(
        string username,
        int minLength,
        int maxLength
    )
    {
        return new UsernameNotValidException(
            $"Username '{username}' must be between {minLength} and {maxLength} characters long."
        );
    }

    public static UsernameNotValidException CreateInvalidCharacters(
        string username,
        string validCharacters
    )
    {
        return new UsernameNotValidException(
            $"Username '{username}' contains invalid characters. Valid characters are: {validCharacters}."
        );
    }

    public static UsernameNotValidException CreateEmpty()
    {
        return new UsernameNotValidException(
            "Username cannot be empty."
        );
    }
}