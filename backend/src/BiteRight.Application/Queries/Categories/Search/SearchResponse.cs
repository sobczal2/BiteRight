// # ==============================================================================
// # Solution: BiteRight
// # File: SearchResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Common;

#endregion

namespace BiteRight.Application.Queries.Categories.Search;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record SearchResponse(PaginatedList<CategoryDto> Categories);