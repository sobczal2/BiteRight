using Auth0.ManagementApi.Paging;
using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Common;

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchResponse
{
    public PaginatedList<CategoryDto> Categories { get; set; }
    
    public SearchResponse(
        PaginatedList<CategoryDto> categories
    )
    {
        Categories = categories;
    }
}