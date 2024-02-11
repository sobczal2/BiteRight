using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Users.Exceptions;

public class UsernameInvalidCharactersException : BusinessRuleDomainException
{
    public UsernameInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}