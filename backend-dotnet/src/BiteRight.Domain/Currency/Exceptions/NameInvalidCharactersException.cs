using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Currency.Exceptions;

public class NameInvalidCharactersException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }
    
    public NameInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}