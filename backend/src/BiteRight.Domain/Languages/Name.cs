using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Languages.Exceptions;
using BiteRight.Utils;

namespace BiteRight.Domain.Languages;

public class Name : ValueObject
{
    public string Value { get; }

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
    private const int MaxLength = 30;
    
    private static readonly Regex ValidCharacters = CommonRegexes.LettersWithSpaces;
    
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