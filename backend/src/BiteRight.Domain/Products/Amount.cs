// # ==============================================================================
// # Solution: BiteRight
// # File: Amount.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;
using BiteRight.Domain.Units;

#endregion

namespace BiteRight.Domain.Products;

public class Amount : Entity<AmountId>
{
    private const double MinValidValue = 0;
    private const double MaxValidValue = 1e6;
    // EF Core
    private Amount()
    {
        CurrentValue = default!;
        MaxValue = default!;
        UnitId = default!;
        Unit = default!;
        ProductId = default!;
        Product = default!;
    }

    private Amount(
        AmountId id,
        double currentValue,
        double maxValue,
        UnitId unitId,
        ProductId productId
    ) : base(id)
    {
        CurrentValue = currentValue;
        MaxValue = maxValue;
        UnitId = unitId;
        Unit = default!;
        ProductId = productId;
        Product = default!;
    }

    public double CurrentValue { get; private set; }
    public double MaxValue { get; private set;  }
    public UnitId UnitId { get; private set;  }
    public virtual Unit Unit { get; }
    public ProductId ProductId { get; }
    public virtual Product Product { get; }

    public static Amount Create(
        double currentValue,
        double maxValue,
        UnitId unitId,
        ProductId productId,
        AmountId? id = null
    )
    {
        Validate(currentValue, maxValue, unitId, productId);

        var amount = new Amount(
            id ?? new AmountId(),
            currentValue,
            maxValue,
            unitId,
            productId
        );

        return amount;
    }

    private static void Validate(
        double currentValue,
        double maxValue,
        UnitId unitId,
        ProductId productId
    )
    {
        if (currentValue is < MinValidValue or > MaxValidValue)
            throw new AmountCurrentValueInvalidValueException(MinValidValue, MaxValidValue);

        if (maxValue is < MinValidValue or > MaxValidValue)
            throw new AmountMaxValueInvalidValueException(MinValidValue, MaxValidValue);

        if (currentValue > maxValue) throw new AmountCurrentValueGreaterThanMaxValueException();
    }

    public static Amount CreateFull(
        double maxValue,
        UnitId unitId,
        ProductId productId
        )
    {
        return Create(maxValue, maxValue, unitId, productId);
    }

    public double GetPercentage()
    {
        return CurrentValue / MaxValue * 100;
    }

    public void ChangeCurrent(
        double amount
    )
    {
        if (amount is < MinValidValue or > MaxValidValue)
            throw new AmountCurrentValueInvalidValueException(MinValidValue, MaxValidValue);

        if (amount > MaxValue) throw new AmountCurrentValueGreaterThanMaxValueException();

        CurrentValue = amount;
    }

    public void UpdateValue(
        double currentValue,
        double maxValue
    )
    {
        Validate(currentValue, maxValue, UnitId, ProductId);
        CurrentValue = currentValue;
        MaxValue = maxValue;
    }

    public void UpdateUnit(
        UnitId unitId
    )
    {
        Validate(CurrentValue, MaxValue, unitId, ProductId);
        UnitId = unitId;
    }
}