namespace BiteRight.Domain.Common;

public abstract class GuidId : Id<Guid>
{
    protected GuidId()
        : base(Guid.NewGuid())
    {
    }

    protected GuidId(
        Guid value
    )
        : base(value)
    {
    }
}