using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Users.Exceptions;

public class UsernameCharactersNotValidException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }
    
    public UsernameCharactersNotValidException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}