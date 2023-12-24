using BiteRight.Domain.Common;

namespace BiteRight.Domain.Currency;

public class Currency : AggregateRoot<CurrencyId>
{
    public Code Code { get; private set; }
    public Name Name { get; private set; }
    public Symbol Symbol { get; private set; }

    // EF Core
    private Currency()
    {
        Code = default!;
        Name = default!;
        Symbol = default!;
    }
    
    private Currency(
        CurrencyId id,
        Code code,
        Name name,
        Symbol symbol
    )
    {
        Code = code;
        Name = name;
        Symbol = symbol;
    }
    
    public static Currency Create(
        Code code,
        Name name,
        Symbol symbol
    )
    {
        return new Currency(
            new CurrencyId(Guid.NewGuid()),
            code,
            name,
            symbol
        );
    }
}