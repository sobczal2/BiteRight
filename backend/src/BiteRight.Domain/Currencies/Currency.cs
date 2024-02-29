// # ==============================================================================
// # Solution: BiteRight
// # File: Currency.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Currencies;

public class Currency : AggregateRoot<CurrencyId>
{
    // EF Core
    private Currency()
    {
        Name = default!;
        Symbol = default!;
        ISO4217Code = default!;
    }

    private Currency(
        CurrencyId id,
        Name name,
        Symbol symbol,
        ISO4217Code iso4217Code,
        bool isDefault
    )
        : base(id)
    {
        Name = name;
        Symbol = symbol;
        ISO4217Code = iso4217Code;
        IsDefault = isDefault;
    }

    public Name Name { get; private set; }
    public Symbol Symbol { get; private set; }

    // ReSharper disable once InconsistentNaming
    public ISO4217Code ISO4217Code { get; private set; }
    public bool IsDefault { get; set; }

    public static Currency Create(
        Name name,
        Symbol symbol,
        ISO4217Code iso4217Code,
        bool isDefault,
        CurrencyId? id = null
    )
    {
        var currency = new Currency(
            id ?? new CurrencyId(),
            name,
            symbol,
            iso4217Code,
            isDefault
        );

        return currency;
    }
}