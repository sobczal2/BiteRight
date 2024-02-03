using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Currencies.Exceptions;

public class ISO4217CodeInvalidCharactersException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }

    public ISO4217CodeInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}