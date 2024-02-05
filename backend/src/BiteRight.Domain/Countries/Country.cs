using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Countries;

public class Country : AggregateRoot<CountryId>
{
    public Name NativeName { get; private set; }
    public Name EnglishName { get; private set; }
    public Alpha2Code Alpha2Code { get; private set; }
    public LanguageId OfficialLanguageId { get; private set; }
    public virtual Language OfficialLanguage { get; private set; }
    public CurrencyId CurrencyId { get; private set; }
    public virtual Currency Currency { get; private set; }

    // EF Core
    private Country()
    {
        NativeName = default!;
        EnglishName = default!;
        Alpha2Code = default!;
        OfficialLanguageId = default!;
        OfficialLanguage = default!;
        CurrencyId = default!;
        Currency = default!;
    }

    private Country(
        CountryId id,
        Name nativeName,
        Name englishName,
        Alpha2Code alpha2Code,
        LanguageId officialLanguageId,
        CurrencyId currencyId
    )
        : base(id)
    {
        EnglishName = englishName;
        NativeName = nativeName;
        Alpha2Code = alpha2Code;
        OfficialLanguageId = officialLanguageId;
        OfficialLanguage = default!;
        CurrencyId = currencyId;
        Currency = default!;
    }

    public static Country Create(
        Name nativeName,
        Name englishName,
        Alpha2Code alpha2Code,
        LanguageId officialLanguageId,
        CurrencyId currencyId,
        IDomainEventFactory domainEventFactory,
        CountryId? id = null
    )
    {
        var country = new Country(
            id ?? new CountryId(),
            nativeName,
            englishName,
            alpha2Code,
            officialLanguageId,
            currencyId
        );

        country.AddDomainEvent(
            domainEventFactory.CreateCountryCreatedEvent(
                country.Id
            )
        );

        return country;
    }
}