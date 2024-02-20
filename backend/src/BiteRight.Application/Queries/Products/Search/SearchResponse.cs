// # ==============================================================================
// # Solution: BiteRight
// # File: ListResponse.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Products;

namespace BiteRight.Application.Queries.Products.Search;

public record SearchResponse(PaginatedList<SimpleProductDto> Products);