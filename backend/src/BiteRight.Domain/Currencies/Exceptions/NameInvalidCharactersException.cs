using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Currencies.Exceptions;

public class NameInvalidCharactersException : BusinessRuleDomainException
{
    public NameInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}