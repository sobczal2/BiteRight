using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public Name Name { get; }
    public Photo? Photo { get; }
    
    // EF Core
    private Category()
    {
        Name = default!;
        Photo = default!;
    }
    
    private Category(
        CategoryId id,
        Name name,
        Photo photo
    )
        : base(id)
    {
        Name = name;
        Photo = photo;
    }
    
    public static Category Create(
        Name name,
        Photo photo,
        IDomainEventFactory domainEventFactory,
        CategoryId? id = null
    )
    {
        var category = new Category(
            id ?? new CategoryId(),
            name,
            photo
        );
        
        category.AddDomainEvent(
            domainEventFactory.CreateCategoryCreatedEvent(
                category.Id
            )
        );
        
        return category;
    }
    
    public Uri GetPhotoUri()
    {
        return Photo?.GetUri() ?? Photo.Default.GetUri();
    }
}