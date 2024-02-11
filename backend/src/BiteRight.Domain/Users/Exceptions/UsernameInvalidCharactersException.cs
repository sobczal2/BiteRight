using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Users.Exceptions;

public class UsernameInvalidCharactersException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }

    public UsernameInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}