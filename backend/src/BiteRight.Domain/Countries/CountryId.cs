using BiteRight.Domain.Common;

namespace BiteRight.Domain.Countries;

public class CountryId : GuidId
{
    public CountryId()
    {
    }

    public CountryId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        CountryId id
    ) => id.Value;

    public static implicit operator CountryId(
        Guid id
    ) => new(id);
}