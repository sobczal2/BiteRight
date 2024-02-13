// # ==============================================================================
// # Solution: BiteRight
// # File: ISO4217CodeInvalidCharactersException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Currencies.Exceptions;

public class ISO4217CodeInvalidCharactersException : BusinessRuleDomainException
{
    public ISO4217CodeInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}