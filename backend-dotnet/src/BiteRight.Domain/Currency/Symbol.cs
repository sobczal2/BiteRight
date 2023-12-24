using System.Text.RegularExpressions;
using BiteRight.Domain.Currency.Exceptions;

namespace BiteRight.Domain.Currency;

public class Symbol
{
    public string Value { get; private set; }
    
    private Symbol(
        string value
    )
    {
        Value = value;
    }
    
    public static Symbol Create(
        string value
    )
    {
        Validate(value);

        return new Symbol(value);
    }
    
    public static Symbol CreateSkipValidation(
        string value
    )
    {
        return new Symbol(value);
    }
    
    private const int MinLength = 1;
    private const int MaxLength = 8;
    
    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new SymbolEmptyException();
        }
        
        if (value.Length is < MinLength or > MaxLength)
        {
            throw new SymbolInvalidLengthException(MinLength, MaxLength);
        }
    }
}