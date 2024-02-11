using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Countries.Exceptions;
using BiteRight.Utils;

namespace BiteRight.Domain.Countries;

public class Alpha2Code : ValueObject
{
    private const int ExactLength = 2;

    private static readonly Regex ValidCharacters = CommonRegexes.UppercaseLetters;

    public string Value { get; }

    private Alpha2Code(
        string value
    )
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Alpha2Code Create(
        string value
    )
    {
        Validate(value);

        return new Alpha2Code(value);
    }

    public static Alpha2Code CreateSkipValidation(
        string value
    )
    {
        return new Alpha2Code(value);
    }

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value)) throw new Alpha2CodeEmptyException();

        if (value.Length != ExactLength) throw new Alpha2CodeInvalidLengthException(ExactLength);

        if (!ValidCharacters.IsMatch(value)) throw new Alpha2CodeInvalidCharactersException(ValidCharacters.ToString());
    }

    public static implicit operator string(
        Alpha2Code alpha2Code
    )
    {
        return alpha2Code.Value;
    }
}