using BiteRight.Domain.Common;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Users;

public class Profile : Entity<ProfileId>
{
    public CurrencyId CurrencyId { get; private set; }
    public virtual Currency Currency { get; private set; }
    public TimeZoneInfo TimeZone { get; private set; }


    // EF Core
    private Profile()
    {
        CurrencyId = default!;
        Currency = default!;
        TimeZone = default!;
    }

    private Profile(
        CurrencyId currencyId,
        TimeZoneInfo timeZone,
        ProfileId id
    )
        : base(id)
    {
        CurrencyId = currencyId;
        Currency = default!;
        TimeZone = timeZone;
    }

    public static Profile Create(
        CurrencyId currencyId,
        TimeZoneInfo timeZone,
        ProfileId? id = null
    )
    {
        return new Profile(
            currencyId,
            timeZone,
            id ?? new ProfileId()
        );
    }

    public void Update(
        CurrencyId currencyId
    )
    {
        CurrencyId = currencyId;
    }
}