using BiteRight.Domain.Common;

namespace BiteRight.Domain.Units;

public class TranslationId : GuidId
{
    public TranslationId()
    {
    }

    public TranslationId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        TranslationId id
    )
    {
        return id.Value;
    }

    public static implicit operator TranslationId(
        Guid id
    )
    {
        return new TranslationId(id);
    }
}