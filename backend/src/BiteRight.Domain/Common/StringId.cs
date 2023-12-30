namespace BiteRight.Domain.Common;

public class StringId : Id<string>
{
    public StringId(
        string value
    )
        : base(value)
    {
    }
}