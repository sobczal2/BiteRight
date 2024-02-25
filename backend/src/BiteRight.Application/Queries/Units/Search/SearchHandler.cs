// # ==============================================================================
// # Solution: BiteRight
// # File: SearchHandler.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Units;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;

#endregion

namespace BiteRight.Application.Queries.Units.Search;

public class SearchHandler : QueryHandlerBase<SearchRequest, SearchResponse>
{
    private readonly ILanguageProvider _languageProvider;
    private readonly IUnitRepository _unitRepository;

    public SearchHandler(
        ILanguageProvider languageProvider,
        IUnitRepository unitRepository
    )
    {
        _languageProvider = languageProvider;
        _unitRepository = unitRepository;
    }

    protected override async Task<SearchResponse> HandleImpl(
        SearchRequest request,
        CancellationToken cancellationToken
    )
    {
        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);
        var searchResult = await _unitRepository.Search(
            request.Query,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            languageId,
            cancellationToken
        );

        var pagedList = new PaginatedList<UnitDto>(
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            searchResult.TotalCount,
            searchResult.Units.Select(unit => UnitDto.FromDomain(unit, languageId))
        );

        return new SearchResponse(pagedList);
    }
}