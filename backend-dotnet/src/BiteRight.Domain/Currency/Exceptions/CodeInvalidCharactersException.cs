using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Currency.Exceptions;

public class CodeInvalidCharactersException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }
    
    public CodeInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}