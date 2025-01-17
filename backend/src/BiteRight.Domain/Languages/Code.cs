// # ==============================================================================
// # Solution: BiteRight
// # File: Code.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Languages.Exceptions;
using BiteRight.Utils;

#endregion

namespace BiteRight.Domain.Languages;

public class Code : ValueObject
{
    private const int ExactLength = 2;

    private static readonly Regex ValidCharacters = CommonRegexes.LowercaseLetters;

    private Code(
        string value
    )
    {
        Value = value;
    }

    public string Value { get; }
    public static Code Default => new("en");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Code Create(
        string value
    )
    {
        Validate(value);

        return new Code(value);
    }

    public static Code CreateSkipValidation(
        string value
    )
    {
        return new Code(value);
    }

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value)) throw new CodeEmptyException();

        if (value.Length != ExactLength) throw new CodeInvalidLengthException(ExactLength);

        if (!ValidCharacters.IsMatch(value)) throw new CodeInvalidCharactersException(ValidCharacters.ToString());
    }

    public static implicit operator string(
        Code code
    )
    {
        return code.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}