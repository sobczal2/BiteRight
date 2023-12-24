using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Currency.Exceptions;

namespace BiteRight.Domain.Currency;

public class Name : ValueObject
{
    public string Value { get; private set; }
    
    private Name(
        string value
    )
    {
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static Name Create(
        string value
    )
    {
        Validate(value);

        return new Name(value);
    }
    
    public static Name CreateSkipValidation(
        string value
    )
    {
        return new Name(value);
    }
    
    private const int MinLength = 3;
    private const int MaxLength = 64;
    
    private static readonly Regex ValidCharacters = new(
        @"^[a-zA-Z0-9_ ]+$",
        RegexOptions.Compiled
    );
    
    private static readonly Regex ValidLength = new(
        @"^.{" + MinLength + "," + MaxLength + "}$",
        RegexOptions.Compiled
    );
    
    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new NameEmptyException();
        }
        
        if (!ValidLength.IsMatch(value))
        {
            throw new NameInvalidLengthException(MinLength, MaxLength);
        }
        
        if (!ValidCharacters.IsMatch(value))
        {
            throw new NameInvalidCharactersException(ValidCharacters.ToString());
        }
    }
}