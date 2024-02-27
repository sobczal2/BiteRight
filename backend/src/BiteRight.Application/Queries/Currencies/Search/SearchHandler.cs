// # ==============================================================================
// # Solution: BiteRight
// # File: SearchHandler.cs
// # Author: Łukasz Sobczak
// # Created: 20-02-2024
// # ==============================================================================

#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Currencies;
using BiteRight.Domain.Abstracts.Repositories;

#endregion

namespace BiteRight.Application.Queries.Currencies.Search;

public class SearchHandler : QueryHandlerBase<SearchRequest, SearchResponse>
{
    private readonly ICurrencyRepository _currencyRepository;

    public SearchHandler(
        ICurrencyRepository currencyRepository
    )
    {
        _currencyRepository = currencyRepository;
    }

    protected override async Task<SearchResponse> HandleImpl(
        SearchRequest request,
        CancellationToken cancellationToken
    )
    {
        var searchResult = await _currencyRepository.Search(
            request.Query,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            cancellationToken
        );

        var pagedList = new PaginatedList<CurrencyDto>(
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            searchResult.TotalCount,
            searchResult.Currencies.Select(currency => new CurrencyDto
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.ISO4217Code,
                Symbol = currency.Symbol
            })
        );

        return new SearchResponse(pagedList);
    }
}