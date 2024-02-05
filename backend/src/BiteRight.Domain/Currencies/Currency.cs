using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Currencies;

public class Currency : AggregateRoot<CurrencyId>
{
    public Name Name { get; private set; }
    public Symbol Symbol { get; private set; }

    // ReSharper disable once InconsistentNaming
    public ISO4217Code ISO4217Code { get; private set; }

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
        ISO4217Code iso4217Code
    )
        : base(id)
    {
        Name = name;
        Symbol = symbol;
        ISO4217Code = iso4217Code;
    }

    public static Currency Create(
        Name name,
        Symbol symbol,
        ISO4217Code iso4217Code,
        IDomainEventFactory domainEventFactory,
        CurrencyId? id = null
    )
    {
        var currency = new Currency(
            id ?? new CurrencyId(),
            name,
            symbol,
            iso4217Code
        );

        currency.AddDomainEvent(
            domainEventFactory.CreateCurrencyCreatedEvent(
                currency.Id
            )
        );

        return currency;
    }
}