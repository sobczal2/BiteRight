using BiteRight.Domain.Common;

namespace BiteRight.Domain.Product;

public class ExpirationDate : ValueObject
{
    public DateOnly Value { get; }
    public ExpirationDateKind Kind { get; }

    private ExpirationDate(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
        Value = value;
        Kind = kind;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static ExpirationDate Create(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
        Validate(value, kind);

        return new ExpirationDate(value, kind);
    }
    
    public static ExpirationDate CreateBestBefore(
        DateOnly value
    )
    {
        return Create(value, ExpirationDateKind.BestBefore);
    }
    
    public static ExpirationDate CreateUseBy(
        DateOnly value
    )
    {
        return Create(value, ExpirationDateKind.UseBy);
    }
    
    public static ExpirationDate CreateUnknown(
        DateOnly value = default
    )
    {
        if(value == default) 
            value = DateOnly.MinValue;
        return Create(value, ExpirationDateKind.Unknown);
    }
    
    public static ExpirationDate CreateInfinite()
    {
        return Create(DateOnly.MaxValue, ExpirationDateKind.Unknown);
    }

    public static ExpirationDate CreateSkipValidation(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
        return new ExpirationDate(value, kind);
    }

    private static void Validate(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
    }
    
    public bool IsInfinite() => Value == DateOnly.MaxValue;

    public enum ExpirationDateKind
    {
        Unknown,
        BestBefore,
        UseBy
    }
}