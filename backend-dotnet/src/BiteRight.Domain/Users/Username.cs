using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Users.Exceptions;

namespace BiteRight.Domain.Users;

public class Username : ValueObject
{
    public string Value { get; }

    private Username(
        string value
    )
    {
        Value = value;
    }
    
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
    
    private const int MinLength = 3;
    private const int MaxLength = 20;

    private static readonly Regex ValidCharacters = new(
        @"^[a-zA-Z0-9_]+$",
        RegexOptions.Compiled
    );
    private static readonly Regex ValidLength = new(
        @"^.{" + MinLength + "," + MaxLength + "}$",
        RegexOptions.Compiled
    );

    public static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new UsernameEmptyException();
        }

        if (!ValidLength.IsMatch(value))
        {
            throw new UsernameLengthNotValidException(MinLength, MaxLength);
        }

        if (!ValidCharacters.IsMatch(value))
        {
            throw new UsernameCharactersNotValidException(ValidCharacters.ToString());
        }
    }
}