namespace BiteRight.Application.Dtos.Categories;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Uri PhotoUri { get; set; } = default!;
}