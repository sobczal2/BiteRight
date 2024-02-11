using System;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Languages;

public class LanguageId : GuidId
{
    public LanguageId()
    {
    }

    public LanguageId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        LanguageId id
    )
    {
        return id.Value;
    }

    public static implicit operator LanguageId(
        Guid id
    )
    {
        return new LanguageId(id);
    }
}