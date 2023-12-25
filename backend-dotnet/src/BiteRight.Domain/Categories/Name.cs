using System.Text.RegularExpressions;
using BiteRight.Domain.Categories.Exceptions;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

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
    }
    
    public static implicit operator string(
        Name name
    )
    {
        return name.Value;
    }
}