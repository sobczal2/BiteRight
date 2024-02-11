using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Units.Exceptions;

public class AbbreviationInvalidCharactersException : BusinessRuleDomainException
{
    public AbbreviationInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}