using BiteRight.Domain.Common;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Users;

public class Profile : Entity<ProfileId>
{
    public CurrencyId CurrencyId { get; private set; }
    public virtual Currency Currency { get; private set; }
    
    
    // EF Core
    private Profile()
    {
        CurrencyId = default!;
        Currency = default!;
    }
    
    private Profile(
        CurrencyId currencyId,
        ProfileId id
    ) : base(id)
    {
        CurrencyId = currencyId;
        Currency = default!;
    }
    
    public static Profile Create(
        CurrencyId currencyId,
        ProfileId? id = null
    )
    {
        return new Profile(
            currencyId,
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