using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Common;

namespace BiteRight.Application.Queries.Categories.Search;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record SearchResponse(PaginatedList<CategoryDto> Categories);