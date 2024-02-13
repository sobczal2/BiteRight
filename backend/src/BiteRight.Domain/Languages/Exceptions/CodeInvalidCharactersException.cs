// # ==============================================================================
// # Solution: BiteRight
// # File: CodeInvalidCharactersException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Languages.Exceptions;

public class CodeInvalidCharactersException : BusinessRuleDomainException
{
    public CodeInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}