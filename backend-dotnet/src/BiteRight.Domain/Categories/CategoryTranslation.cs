using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Categories;

public class CategoryTranslation : Entity<CategoryTranslationId>
{
    public CategoryId CategoryId { get; private set; }
    public LanguageId LanguageId { get; private set; }
    public Name Name { get; private set; }
    
    // EF Core
    private CategoryTranslation()
    {
        CategoryId = default!;
        Name = default!;
        LanguageId = default!;
    }
    
    private CategoryTranslation(
        CategoryTranslationId id,
        CategoryId categoryId,
        Name name,
        LanguageId languageId
    )
        : base(id)
    {
        CategoryId = categoryId;
        Name = name;
        LanguageId = languageId;
    }
    
    public static CategoryTranslation Create(
        CategoryId categoryId,
        Name name,
        LanguageId languageId,
        CategoryTranslationId? id = null
    )
        => new(
            id ?? new CategoryTranslationId(),
            categoryId,
            name,
            languageId
        );
}