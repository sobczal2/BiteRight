// # ==============================================================================
// # Solution: BiteRight
// # File: SearchRequest.cs
// # Author: Łukasz Sobczak
// # Created: 20-02-2024
// # ==============================================================================

using BiteRight.Application.Dtos.Common;
using MediatR;

namespace BiteRight.Application.Queries.Currencies.Search;

public class SearchRequest : IRequest<SearchResponse>
{
    public SearchRequest()
    {
        Query = string.Empty;
        PaginationParams = PaginationParams.Default;
    }

    public string Query { get; set; }
    public PaginationParams PaginationParams { get; set; }
}