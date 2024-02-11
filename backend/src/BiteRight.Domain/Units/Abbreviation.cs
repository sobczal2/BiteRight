using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Units.Exceptions;
using BiteRight.Utils;

namespace BiteRight.Domain.Units;

public class Abbreviation : ValueObject
{
    public string Value { get; }

    private Abbreviation(
        string value
    )
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Abbreviation Create(
        string value
    )
    {
        Validate(value);

        return new Abbreviation(value);
    }

    public static Abbreviation CreateSkipValidation(
        string value
    )
    {
        return new Abbreviation(value);
    }

    private const int MinLength = 1;
    private const int MaxLength = 5;

    private static readonly Regex ValidCharacters = CommonRegexes.Letters;

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new AbbreviationEmptyException();
        }

        if (value.Length is < MinLength or > MaxLength)
        {
            throw new AbbreviationInvalidLengthException(MinLength, MaxLength);
        }

        if (!ValidCharacters.IsMatch(value))
        {
            throw new AbbreviationInvalidCharactersException(ValidCharacters.ToString());
        }
    }

    public static implicit operator string(
        Abbreviation name
    )
    {
        return name.Value;
    }
}