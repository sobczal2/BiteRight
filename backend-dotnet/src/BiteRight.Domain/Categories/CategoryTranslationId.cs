using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class CategoryTranslationId : GuidId
{
    public CategoryTranslationId()
    {
    }

    public CategoryTranslationId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator CategoryTranslationId(
        Guid id
    )
        => new(id);

    public static implicit operator Guid(
        CategoryTranslationId id
    )
        => id.Value;
}