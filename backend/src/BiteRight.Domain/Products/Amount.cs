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
    // EF Core
    private Amount()
    {
        CurrentValue = default!;
        MaxValue = default!;
        UnitId = default!;
    }

    private Amount(
        AmountId id,
        double currentValue,
        double maxValue,
        UnitId unitId
    ) : base(id)
    {
        CurrentValue = currentValue;
        MaxValue = maxValue;
        UnitId = unitId;
    }

    public double CurrentValue { get; private set; }
    public double MaxValue { get; }
    public UnitId UnitId { get; private set; }

    public static Amount Create(
        double currentValue,
        double maxValue,
        UnitId unitId,
        AmountId? id = null
    )
    {
        ValidateAmount(currentValue, maxValue);

        var amount = new Amount(
            id ?? new AmountId(),
            currentValue,
            maxValue,
            unitId
        );

        return amount;
    }

    private static void ValidateAmount(
        double currentValue,
        double maxValue
    )
    {
        if (currentValue < 0) throw new AmountCurrentValueLessThanZeroException();

        if (maxValue < 0) throw new AmountMaxValueLessThanZeroException();

        if (currentValue > maxValue) throw new AmountCurrentValueGreaterThanMaxValueException();
    }

    public static Amount CreateFull(UnitId unitId, double maxValue)
    {
        return Create(maxValue, maxValue, unitId);
    }

    public double GetPercentage()
    {
        return CurrentValue / MaxValue * 100;
    }

    public void ChangeCurrent(
        double amount
    )
    {
        if (amount < 0) throw new AmountCurrentValueLessThanZeroException();

        if (amount > MaxValue) throw new AmountCurrentValueGreaterThanMaxValueException();

        CurrentValue = amount;
    }
}