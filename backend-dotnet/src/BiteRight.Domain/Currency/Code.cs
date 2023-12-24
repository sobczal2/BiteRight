using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Currency.Exceptions;

namespace BiteRight.Domain.Currency;

public class Code : ValueObject
{
    public string Value { get; }
    
    private Code(
        string value
    )
    {
        Value = value;
    }
    
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
    
    private const int ExactLength = 3;
    
    private static readonly Regex ValidCharacters = new(
        @"^[A-Z]+$",
        RegexOptions.Compiled
    );

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new CodeEmptyException();
        }

        if (value.Length != ExactLength)
        {
            throw new CodeInvalidLengthException(ExactLength);
        }

        if (!ValidCharacters.IsMatch(value))
        {
            throw new CodeInvalidCharactersException(ValidCharacters.ToString());
        }
    }
}