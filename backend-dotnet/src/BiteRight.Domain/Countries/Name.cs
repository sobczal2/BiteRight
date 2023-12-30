using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Countries.Exceptions;

namespace BiteRight.Domain.Countries;

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
    private const int MaxLength = 100;
    
    private static readonly Regex ValidCharacters = new(
        @"^[a-zA-Z\s]+$",
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
        
        if (value.Length is < MinLength or > MaxLength)
        {
            throw new NameInvalidLengthException(MinLength, MaxLength);
        }
        
        if (!ValidCharacters.IsMatch(value))
        {
            throw new NameInvalidCharactersException(ValidCharacters.ToString());
        }
    }
    
    public static implicit operator string(
        Name name
    )
    {
        return name.Value;
    }
}