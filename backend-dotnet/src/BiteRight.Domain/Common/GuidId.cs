namespace BiteRight.Domain.Common;

public class GuidId : Id<Guid>
{
    public GuidId(
        Guid value
    )
        : base(value)
    {
    }
}