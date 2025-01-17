// # ==============================================================================
// # Solution: BiteRight
// # File: GetDefaultResponse.cs
// # Author: Łukasz Sobczak
// # Created: 19-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Categories;

#endregion

namespace BiteRight.Application.Queries.Categories.GetDefault;

public class GetDefaultResponse
{
    public CategoryDto Category { get; set; }
    
    public GetDefaultResponse(CategoryDto category)
    {
        Category = category;
    }
}