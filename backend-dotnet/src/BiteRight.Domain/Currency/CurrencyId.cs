using BiteRight.Domain.Common;

namespace BiteRight.Domain.Currency;

public class CurrencyId : GuidId
{
    public CurrencyId(
        Guid value
    )
        : base(value)
    {
    }
}