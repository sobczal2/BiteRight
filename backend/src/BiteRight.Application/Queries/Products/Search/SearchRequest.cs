// # ==============================================================================
// # Solution: BiteRight
// # File: ListRequest.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

using System;
using System.Collections.Generic;
using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Products;
using MediatR;

namespace BiteRight.Application.Queries.Products.Search;

public class SearchRequest : IRequest<SearchResponse>
{
    public SearchRequest()
    {
        Query = string.Empty;
        FilteringParams = FilteringParams.Default;
        SortingStrategy = ProductSortingStrategy.Default;
        PaginationParams = PaginationParams.Default;
    }
    public string Query { get; set; }
    public FilteringParams FilteringParams { get; set; }
    public ProductSortingStrategy SortingStrategy { get; set; }
    public PaginationParams PaginationParams { get; set; }
}

public class FilteringParams
{
    public FilteringParams(
        List<Guid> categoryIds
    )
    {
        CategoryIds = categoryIds;
    }

    public List<Guid> CategoryIds { get; init; }
    
    public static FilteringParams Default => new(new List<Guid>());
}