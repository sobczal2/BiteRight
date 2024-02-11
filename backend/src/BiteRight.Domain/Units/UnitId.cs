using BiteRight.Domain.Common;

namespace BiteRight.Domain.Units;

public class UnitId : GuidId
{
    public UnitId()
    {
    }

    public UnitId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        UnitId id
    ) => id.Value;

    public static implicit operator UnitId(
        Guid id
    ) => new(id);
}