using BiteRight.Domain.Common;

namespace BiteRight.Domain.Currency;

public class Currency : AggregateRoot<CurrencyId>
{
    public Code Code { get; private set; }
    public Name Name { get; private set; }

    // EF Core
    private Currency()
    {
        Code = default!;
        Name = default!;
    }
    
    private Currency(
        CurrencyId id,
        Code code,
        Name name
    )
    {
        Code = code;
        Name = name;
    }
    
    public static Currency Create(
        Code code,
        Name name
    )
    {
        return new Currency(
            new CurrencyId(Guid.NewGuid()),
            code,
            name
        );
    }
}