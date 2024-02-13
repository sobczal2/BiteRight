// # ==============================================================================
// # Solution: BiteRight
// # File: SearchRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Common;
using MediatR;

#endregion

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchRequest : IRequest<SearchResponse>
{
    public SearchRequest(
        string query,
        PaginationParams paginationParams
    )
    {
        Query = query;
        PaginationParams = paginationParams;
    }

    public string Query { get; set; }
    public PaginationParams PaginationParams { get; set; }
}