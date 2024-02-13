// # ==============================================================================
// # Solution: BiteRight
// # File: UsernameInvalidCharactersException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Users.Exceptions;

public class UsernameInvalidCharactersException : BusinessRuleDomainException
{
    public UsernameInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}