using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Units.Exceptions;

public class AbbreviationInvalidCharactersException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }

    public AbbreviationInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}