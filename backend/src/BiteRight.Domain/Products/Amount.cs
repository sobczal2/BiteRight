using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;
using BiteRight.Domain.Units;

namespace BiteRight.Domain.Products;

public class Amount : Entity<AmountId>
{
    public double CurrentValue { get; }
    public double MaxValue { get; }

    public UnitId UnitId { get; private set; }

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
}