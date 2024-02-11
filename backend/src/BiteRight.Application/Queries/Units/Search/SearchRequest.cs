using BiteRight.Application.Dtos.Common;
using MediatR;

namespace BiteRight.Application.Queries.Units.Search;

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