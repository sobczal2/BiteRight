using System.Collections.Generic;
using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies.Exceptions;
using BiteRight.Utils;

namespace BiteRight.Domain.Currencies;

public class ISO4217Code : ValueObject
{
    private const int ExactLength = 3;

    private static readonly Regex ValidCharacters = CommonRegexes.UppercaseLetters;

    private ISO4217Code(
        string value
    )
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static ISO4217Code Create(
        string value
    )
    {
        Validate(value);

        return new ISO4217Code(value);
    }

    public static ISO4217Code CreateSkipValidation(
        string value
    )
    {
        return new ISO4217Code(value);
    }

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ISO4217CodeEmptyException();

        if (value.Length != ExactLength) throw new ISO4217CodeInvalidLengthException(ExactLength);

        if (!ValidCharacters.IsMatch(value))
            throw new ISO4217CodeInvalidCharactersException(ValidCharacters.ToString());
    }

    public static implicit operator string(
        ISO4217Code iso4217Code
    )
    {
        return iso4217Code.Value;
    }
}