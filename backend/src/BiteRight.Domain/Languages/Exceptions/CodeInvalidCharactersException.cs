using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Languages.Exceptions;

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