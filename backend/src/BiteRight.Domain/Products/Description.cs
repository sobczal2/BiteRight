using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Products;

public class Description : ValueObject
{
    public string Value { get; }

    private Description(
        string value
    )
    {
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static Description Create(
        string value
    )
    {
        Validate(value);
        
        return new Description(value);
    }
    
    public static Description CreateSkipValidation(
        string value
    )
    {
        return new Description(value);
    }
    
    private const int MinLength = 3;
    private const int MaxLength = 64;
    
    private static readonly Regex ValidCharacters = new(
        @"^[a-zA-Z\s]+$",
        RegexOptions.Compiled
    );
    
    private static void Validate(
        string value
    )
    {
        if (value.Length is < MinLength or > MaxLength)
        {
            throw new DescriptionInvalidLengthException(MinLength, MaxLength);
        }
        
        if (!ValidCharacters.IsMatch(value))
        {
            throw new DescriptionInvalidCharactersException(ValidCharacters.ToString());
        }
    }
    
    public static implicit operator string(
        Description description
    )
    {
        return description.Value;
    }
}