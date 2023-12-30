using BiteRight.Application.Dtos.Common;
using MediatR;

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchRequest : IRequest<SearchResponse>
{
    public string Query { get; set; }
    public PaginationParams PaginationParams { get; set; }

    public SearchRequest(
        string query,
        PaginationParams paginationParams
    )
    {
        Query = query;
        PaginationParams = paginationParams;
    }
}