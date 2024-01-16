using BiteRight.Domain.Common;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Users;

public class Profile : Entity<ProfileId>
{
    public CountryId CountryId { get; private set; }
    public LanguageId LanguageId { get; private set; }
    public CurrencyId CurrencyId { get; private set; }
    
    // EF Core
    private Profile()
    {
        CountryId = default!;
        LanguageId = default!;
        CurrencyId = default!;
    }
    
    private Profile(
        CountryId countryId,
        LanguageId languageId,
        CurrencyId currencyId,
        ProfileId id
    ) : base(id)
    {
        CountryId = countryId;
        LanguageId = languageId;
        CurrencyId = currencyId;
    }
    
    public static Profile Create(
        CountryId countryId,
        LanguageId languageId,
        CurrencyId currencyId,
        ProfileId? id = null
    )
    {
        return new Profile(
            countryId,
            languageId,
            currencyId,
            id ?? new ProfileId()
        );
    }
}