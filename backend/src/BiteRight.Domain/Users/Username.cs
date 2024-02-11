using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Users.Exceptions;
using BiteRight.Utils;

namespace BiteRight.Domain.Users;

public class Username : ValueObject
{
    private const int MinLength = 3;
    private const int MaxLength = 30;

    private static readonly Regex ValidCharacters = CommonRegexes.AlphanumericWithHuphensAndUnderscores;

    private Username(
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

    public static Username Create(
        string value
    )
    {
        Validate(value);

        return new Username(value);
    }

    public static Username CreateSkipValidation(
        string value
    )
    {
        return new Username(value);
    }

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value)) throw new UsernameEmptyException();

        if (value.Length is < MinLength or > MaxLength) throw new UsernameInvalidLengthException(MinLength, MaxLength);

        if (!ValidCharacters.IsMatch(value)) throw new UsernameInvalidCharactersException(ValidCharacters.ToString());
    }

    public static implicit operator string(
        Username username
    )
    {
        return username.Value;
    }
}