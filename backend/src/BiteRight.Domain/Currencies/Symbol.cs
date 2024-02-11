using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies.Exceptions;

namespace BiteRight.Domain.Currencies;

public class Symbol : ValueObject
{
    private const int MinLength = 1;
    private const int MaxLength = 5;

    public string Value { get; }

    private Symbol(
        string value
    )
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
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

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value)) throw new SymbolEmptyException();

        if (value.Length is < MinLength or > MaxLength) throw new SymbolInvalidLengthException(MinLength, MaxLength);
    }

    public static implicit operator string(
        Symbol symbol
    )
    {
        return symbol.Value;
    }
}